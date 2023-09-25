using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointClickManager : MonoBehaviour
{

    public static PointClickManager instance;

    [Header("General Settings")]
    public GameObject StartPosition;
    public float animationSpeed = 3.0f;
    public bool debugMode;

    [Header("Debug Settings")]
    public bool debugShowCams = false;
    public GameObject debugCamPosition;
    public GameObject debugPlayer;
    public GameObject backButton;
    public GameObject rotateButton;

    [Header("Free look settings")]
    public float mouseSensitivity = 400f;
    public bool FreeLookActive = false;



    private bool ableToGoBack;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private List<GameObject> autoTargetsList;
    private List<GameObject> positionList;
    private int positionCurrent;
    private GameObject backModel;

    bool backEnabled = false;
    bool autoTargetBusy = false;
    private bool NextFreeLook = false;

    
    public float mouseY;

    private void Awake()
    {
        instance = this;
        positionCurrent = 0;
        positionList = new List<GameObject>();
        autoTargetsList = new List<GameObject>();

        if (StartPosition != null)
        {
            addToPositionList(StartPosition);
        }
        else
        {
            Debug.Log("PointClickCamStart is not set on PointClickManager");
        }
    }

    public void gotoPosition(GameObject Model)
    {
        if (eventSystemFound())
        {
            // This prevents clicking trough the UI
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                addToPositionList(Model);
            }
        }
        else
        {
            addToPositionList(Model);
        }
    }

    bool eventSystemFound()
    {
        // Get all GameObjects in the scene and search for EventSystem
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        bool eventSystemFound = false;

        // Iterate through all GameObjects and check if they have an EventManager component
        foreach (GameObject gameObject in allGameObjects)
        {
            if (gameObject.GetComponent<EventSystem>() != null)
            {
                eventSystemFound = true;
                break;
            }
        }

        return eventSystemFound;
    }

    public void addToPositionList(GameObject go)
    {

        // Goto the next position with coordinates from Model
        NextFreeLook = go.GetComponent<PointClickCam>().FreeView;
        FreeLookActive = false;

        // Enable or Disable the rotate GUI image
        if (rotateButton != null)
        {
            rotateButton.SetActive(false);
        }

        // Add the new position to the list
        positionList.Add(go);
        positionCurrent = positionList.Count-1;

        // During movement, not able to go back imediatly
        backEnabled = false;

        // If there is a Back model, set it up here
        if (go.GetComponent<PointClickCam>().backModel != null)
        {
            ableToGoBack = true;
            backModel = go.GetComponent<PointClickCam>().backModel;
        }
        else
        {
            ableToGoBack = false;
        }

        // Enable or Disable the back GUI button
        if (backButton != null)
        {
            backButton.SetActive(ableToGoBack);
        }

        // If the model contains automated stops, the auto target list is filled
        if (go.GetComponent<PointClickCam>().GroupAnimation != null && !autoTargetBusy)
        {
            autoTargetsList.Clear();
            foreach (GameObject t in go.GetComponent<PointClickCam>().GroupAnimation)
            {
                autoTargetsList.Add(t);
            }

            if (autoTargetsList.Count>0)
            {
                StartCoroutine(handleAutoTargets());
            }
        }
        
    }

    IEnumerator handleAutoTargets()
    {
        autoTargetBusy = true;

        while (autoTargetsList.Count > 0)
        {
            // Go to automated cam points
            yield return new WaitForSeconds(0.5f);
            addToPositionList(autoTargetsList[0]);
            autoTargetsList.RemoveAt(0);

            // Enable or Disable the back GUI button
            if (backButton != null)
            {
                backButton.SetActive(false);
            }
        }

        yield return null;

        // Enable or Disable the back GUI button
        if (backButton != null)
        {
            backButton.SetActive(ableToGoBack);
        }

        autoTargetBusy = false;

    }

    public void gotoPreviousPosition()
    {
        // If possible, the backmodel is added to the next position
        if (ableToGoBack && backEnabled)
        {
            addToPositionList(backModel);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            gotoPreviousPosition();
        }

        #region CAMERA MOVEMENT


        if (!FreeLookActive)
        {
            float step = animationSpeed * Time.deltaTime;

            Transform newtrans;

            if (positionList.Count > 0)
            {
                float offsetY = positionList[positionCurrent].GetComponent<PointClickCam>().PositionOffsetY;
                Vector3 newPos = new Vector3(positionList[positionCurrent].transform.position.x,
                    positionList[positionCurrent].transform.position.y + offsetY,
                    positionList[positionCurrent].transform.position.z);
                Quaternion newRot = positionList[positionCurrent].transform.rotation;

                targetPosition = newPos;
                targetRotation = newRot;
            }

            if (debugMode)
            {
                newtrans = debugPlayer.transform;

                Camera.main.transform.position = debugCamPosition.transform.position;
                Camera.main.transform.rotation = debugCamPosition.transform.rotation;
            }
            else
            {
                newtrans = Camera.main.transform;
            }
            

            if (Vector3.Distance(newtrans.transform.position, targetPosition) < 0.5f)
            {
                backEnabled = true;
            }

            if (Vector3.Distance(newtrans.transform.position, targetPosition) < 0.2f)
            {
                // If set, the camera can be rotated just before entering the very last position
                setUpCameraRotation();
            }

            // Actual movement of the Camera
            if (Vector3.Distance(newtrans.transform.position, targetPosition) < .001f)
            {
                newtrans.transform.position = targetPosition;
                newtrans.transform.rotation = targetRotation;

                setUpCameraRotation();

                NextFreeLook = false;
            }
            else
            {
                newtrans.transform.position = Vector3.Lerp(newtrans.transform.position, targetPosition, step);
                newtrans.transform.rotation = Quaternion.Slerp(newtrans.transform.rotation, targetRotation, step);
            }
        }

        void setUpCameraRotation()
        {
            if (!debugMode)
            {
                FreeLookActive = NextFreeLook;
                if (FreeLookActive)
                {
                    if (Camera.main.GetComponent<CameraRotate>() != null)
                    {
                        Camera.main.GetComponent<CameraRotate>().SetCurrentRotation();
                    }
                }

                // Enable or Disable the rotate GUI image
                if (rotateButton != null)
                {
                    rotateButton.SetActive(FreeLookActive);
                }

            }
        }

        #endregion
    }
}
