using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightUpText : MonoBehaviour {
    Light ligTxt;
    public bool turnedOn = false;
    int countIdx = 0;
	// Use this for initialization
	void Start () {
        ligTxt = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if(turnedOn && countIdx < 70){
            lightUp();
            countIdx++;
        }

	}

    void lightUp(){
        if (ligTxt.intensity < 60)
        {
            ligTxt.intensity += 2;
        }
    }
}
