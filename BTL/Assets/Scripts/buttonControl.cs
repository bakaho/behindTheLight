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
        GameManager.curShapeM = GameManager.shapeM;
        GameManager.curShapeS = GameManager.shapeS;
        print(GameManager.curShapeM + " + " + GameManager.curShapeS);
        lightPlayer.GetComponent<Light>().cookie = lightCookie;
        //hide button
        btn.gameObject.SetActive(false);
    }
}
