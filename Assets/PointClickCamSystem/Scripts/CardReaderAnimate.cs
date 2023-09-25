using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReaderAnimate : MonoBehaviour
{
    public Animator anim;
    public string AnimTrigger;
    public GameObject model;

    public bool PointAndCLickBack = false;
    public GameObject PointAndClickDeactivate;
    public Animator animOther;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {

        if ( GameManager.instance.KeyCardPickedUp)
        {
            if (!GameManager.instance.KeyCardUsed)
            {
                GameManager.instance.RemoveKeyCard();
                if (PointAndClickDeactivate != null)
                {
                    PointAndClickDeactivate.SetActive(false);
                }

                doKeyCardAnimation();
                if (PointAndCLickBack)
                {
                    StartCoroutine(goBack());
                }
            }
        }
    }

    IEnumerator goBack()
    {
        yield return new WaitForSeconds(1.5f);
        PointClickManager.instance.gotoPreviousPosition();
        yield return null;
    }

    public void doKeyCardAnimation()
    {
        anim.SetTrigger(AnimTrigger);
        StartCoroutine(modelStart());
    }

    IEnumerator modelStart()
    {
        yield return new WaitForSeconds(1.2f);

        if (animOther != null)
        {
            animOther.SetTrigger(AnimTrigger);
        }

        yield return null;
    }

}
