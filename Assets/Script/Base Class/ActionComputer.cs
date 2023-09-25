using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class ActionComputer : Actions
{
    [SerializeField]
    AudioClip[] tabsounds;
    [SerializeField]
    FlashImages flashImages;

    bool triggered = false;
    
    public override void Act()
    {
        if (triggered == false)
            triggered = true;
        else
            return;
        AudioSource audio_computer = GetComponent<AudioSource>();
       // audio_computer.AudioClip = tabsounds[0];
        audio_computer.clip = tabsounds[0];
        audio_computer.Play();
        StartCoroutine(PlaySecondClipAfterDelay(47.5f, audio_computer));
        
    }
    private IEnumerator PlaySecondClipAfterDelay(float delay, AudioSource audio_computer)
    {
        
        yield return new WaitForSeconds(delay);
        audio_computer.clip = tabsounds[1];
        audio_computer.Play();
        StartCoroutine(flashImages.ShowImageSucc());
        StartCoroutine(PlayThirdClip(25.5f,audio_computer)); 
        
    }
    private IEnumerator PlayThirdClip(float delay, AudioSource audio_computer)
    {
         yield return new WaitForSeconds(delay);
       SceneManager.LoadScene("SceneInter");

    }
}
