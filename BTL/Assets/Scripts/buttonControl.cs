using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonControl : MonoBehaviour {
    static public Texture lightCookie;
    public GameObject lightPlayer;
    public Button btn;

    public Button btnOff;
    public Button btnOn;
    public Image statusBar;


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

    public void showBar(){
        //print("btn!!!");
        //cornerBtnOn = !cornerBtnOn;
        btnOff.gameObject.SetActive(false);
        btnOn.gameObject.SetActive(true);
        statusBar.gameObject.SetActive(true);
        //cornerSpriteNum = (cornerSpriteNum + 1) % 2;
        //cornerBtn.GetComponent<Image>().sprite = crnImg[cornerSpriteNum];
        
    }

    //public void hideBar()
    //{
    //    //print("btn!!!");
    //    cornerBtnOn = !cornerBtnOn;
    //    statusBar.gameObject.SetActive(cornerBtnOn);
    //    cornerSpriteNum = (cornerSpriteNum + 1) % 2;
    //    cornerBtn.GetComponent<Image>().sprite = crnImg[cornerSpriteNum];

    //}

    public void hideBar()
    {
        btnOff.gameObject.SetActive(true);
        btnOn.gameObject.SetActive(false);
        statusBar.gameObject.SetActive(false);

    }
}
