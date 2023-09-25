using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera mainCamera;

    private bool turning;
    private Quaternion targetRot;

	// Use this for initialization
	void Start ()
    {
        mainCamera = Camera.main;


	}

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick();


	}

    void OnClick()
    {
        Debug.Log("Left Clicked!");

        RaycastHit hit;
        Ray camToScreen = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camToScreen, out hit, Mathf.Infinity))
        {
            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                 

                    interactable.Interact();
                }

            }
        }
    }



}
