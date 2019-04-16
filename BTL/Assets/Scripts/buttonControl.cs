using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonControl : MonoBehaviour {
    static public Texture lightCookie;
    public GameObject lightPlayer;
    public Button btn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeShape(){
        lightPlayer.GetComponent<Light>().cookie = lightCookie;
        //TODO: hide button
        btn.gameObject.SetActive(false);
    }
}
