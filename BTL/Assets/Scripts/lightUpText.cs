using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightUpText : MonoBehaviour {
    Light ligTxt;

	// Use this for initialization
	void Start () {
        ligTxt = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if(ligTxt.intensity<60){
            ligTxt.intensity+=2;
        }
	}
}
