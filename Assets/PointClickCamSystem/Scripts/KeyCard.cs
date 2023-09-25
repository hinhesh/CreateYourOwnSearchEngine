using UnityEngine;

public class KeyCard : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "KeyCard")
                {
                    if (hitInfo.distance < 1.0f)
                    {
                        GameManager.instance.PickupKeyCard();
                    }
                }
            }
        }

    }
}
