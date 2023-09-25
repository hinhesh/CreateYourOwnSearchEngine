using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashImages : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartFlashImages()
    {
         Transform parentTransform = transform;
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            StartCoroutine(ShowImage(10f, child));
        }
    }
    public IEnumerator ShowImageSucc()
    {  
        Transform parentTransform = transform;
        foreach (Transform child in transform)
        {
           
            yield return new WaitForSeconds(.4f);
             child.gameObject.SetActive(true);
              yield return new WaitForSeconds(.3f);
            //StartCoroutine(ShowImage(10f, child));
        }

    }
    private IEnumerator ShowImage(float delay, Transform child)
    {
         yield return new WaitForSeconds(delay);
         child.gameObject.SetActive(true);
         
    }
}
