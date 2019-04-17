using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotControl : MonoBehaviour {
    public bool isTriggered = false; //if an item in


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeItemImg(Sprite sp){
        this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = sp;
    }
    public void turnOn(){
        this.gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        isTriggered = true;
    }

}
