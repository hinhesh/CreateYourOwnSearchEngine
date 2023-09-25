using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float distancePosition = 1f;
    [SerializeField] Actions[] actions;

    public Vector3 InteractPosition()
    {
        return transform.position + transform.forward * distancePosition;
    }

    public void Interact()
    {
       

        WaitforPlayerArriving();
    }

    void WaitforPlayerArriving()
    {

	//	player.SetDirection(transform.position);
        foreach (Actions a in actions)
        {
            a.Act();
        }
    }
}
