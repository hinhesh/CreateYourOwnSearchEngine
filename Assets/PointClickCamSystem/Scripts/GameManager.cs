using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject KeyCard;
    public GameObject KeyCard_HUD;

    public bool KeyCardPickedUp;
    public bool KeyCardUsed;

    private void Awake()
    {
        instance = this;
        KeyCard_HUD.SetActive(false);
        KeyCardPickedUp = false;
        KeyCardUsed = false;
    }

    public void PickupKeyCard()
    {
        if (!KeyCardPickedUp)
        {
            KeyCardPickedUp = true;
            KeyCard.SetActive(false);
            KeyCard_HUD.SetActive(true);
        }
    }

    public void RemoveKeyCard()
    {
        KeyCardUsed = true;
        KeyCard_HUD.SetActive(false);
    }

}
