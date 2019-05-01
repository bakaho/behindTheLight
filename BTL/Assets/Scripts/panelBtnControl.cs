using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelBtnControl : MonoBehaviour {
    public GameObject panel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void closeMe(){
        panel.gameObject.SetActive(false);
    }
}
