using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSound : MonoBehaviour {
    [Header("Sound")]
    AudioSource[] sources;
    AudioClip[] clips;



    void OnTriggerEnter(Collider mySound)
    {
        if (!GameManager.isPaused)
        {
            if (mySound.gameObject.CompareTag("soundEffect"))
            {
                if(!mySound.GetComponent<soundEffectControl>().isPlayed){
                    mySound.GetComponent<AudioSource>().PlayOneShot(mySound.GetComponent<soundEffectControl>().myClip);
                }
            }
        }
    }
}
