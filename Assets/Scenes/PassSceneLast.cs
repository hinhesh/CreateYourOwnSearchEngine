using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class PassSceneLast : MonoBehaviour
{
    public void FinalScene()
    { 
        SceneManager.LoadScene("Scene3");
    } 
}
