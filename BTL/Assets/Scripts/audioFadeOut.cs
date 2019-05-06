using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioFadeOut : MonoBehaviour {
    
    public void fade(){
        AudioSource sound = GetComponent<AudioSource>();
        StartCoroutine(FadeOut(this.gameObject, sound, 3));


    }

    public static IEnumerator FadeOut(GameObject obj, AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
        obj.SetActive(false);
        print("run");

    }
}
