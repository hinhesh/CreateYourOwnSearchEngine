using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadNextScene()
    {
         SceneManager.LoadScene("SCENE2 1");
    }
}
