using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Restart : MonoBehaviour, IPointerClickHandler
    // Start is called before the first frame update

 {

   public void Reload()
   {
       SceneManager.LoadScene("SceneIntro");
   }
    public void OnPointerClick(PointerEventData eventData)
    {
       ///SceneManager.LoadScene("SampleScene");
    }
}   

