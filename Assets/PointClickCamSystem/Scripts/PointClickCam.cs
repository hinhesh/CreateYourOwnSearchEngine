using UnityEngine;

public class PointClickCam : MonoBehaviour
{
    public GameObject Model;
    public GameObject backModel;

    public GameObject[] GroupAnimation;

    public bool FreeView;
    public float PositionOffsetY = 1.5f;
    

    void OnMouseDown()
    {
        PointClickManager.instance.gotoPosition(Model);
       
        if (this.GetComponent<AudioSource>() != null)
            this.GetComponent<AudioSource>().Play();
        if (backModel != null && this.backModel.GetComponent<AudioSource>() != null)
        this.backModel.GetComponent<AudioSource>().Pause();
    }

    #region DEBUG INFO

    private void Start()
    {
        if (!PointClickManager.instance.debugShowCams)
        {
            Component[] childs;
            childs = GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer child in childs)
            {
                child.enabled = false;
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (Model.GetComponent<PointClickCam>().backModel != null)
        {
            Vector3 pos1 = new Vector3(
                Model.GetComponent<PointClickCam>().transform.position.x,
                Model.GetComponent<PointClickCam>().transform.position.y + Model.GetComponent<PointClickCam>().PositionOffsetY,
                Model.GetComponent<PointClickCam>().transform.position.z);

            Vector3 pos2 = new Vector3(
                Model.GetComponent<PointClickCam>().backModel.transform.position.x,
                Model.GetComponent<PointClickCam>().backModel.transform.position.y + Model.GetComponent<PointClickCam>().PositionOffsetY,
                Model.GetComponent<PointClickCam>().backModel.transform.position.z);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pos1, pos2);
        }

        if (Model.GetComponent<PointClickCam>().GroupAnimation.Length > 0 )
        {

            Gizmos.DrawSphere(Model.GetComponent<PointClickCam>().transform.position, 0.1f);

            foreach (GameObject t in Model.GetComponent<PointClickCam>().GroupAnimation)
            {
                Gizmos.DrawSphere(t.transform.position, 0.1f);
            }


        }

    }

    #endregion
}
