using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkerDotControl : MonoBehaviour {
    public bool isOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setOn(){
        isOn = true;
        this.GetComponent<Image>().color = new Color32(13, 255, 227, 255);

    }
}
