using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonControl : MonoBehaviour {
    [Header("Cookie Button")]
    static public Texture lightCookie;
    public GameObject lightPlayer;
    public Button btn;

    [Header("Corner Button")]
    public Button btnOff;
    public Button btnOn;
    public Image statusBar;

    [Header("Settings Button")]
    public Button btnSettings;
    public Image settingsBar;

    [Header("Pause Button")]
    public Button btnPause;
    public Sprite toPause;
    public Sprite toResume;
    public bool pausing = false;

    [Header("Sound Button")]
    public Button btnSound;
    public Sprite bsOn;
    public Sprite bsOff;
    public bool muted = false;

    [Header("Quit Button")]
    public Button btnQuit;

    [Header("Guide")]
    public Animator inventGuide;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeShape(){
        GameManager.curShapeM = GameManager.shapeM;
        GameManager.curShapeS = GameManager.shapeS;
        GameManager.curShapeI = GameManager.shapeI;
        print(GameManager.curShapeM + " + " + GameManager.curShapeS + " + " + GameManager.curShapeI);
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
        if(PlayerPrefs.GetInt(GameManager.guidedKey) == 0){
            inventGuide.SetBool("hide", true);
            PlayerPrefs.SetInt(GameManager.guidedKey, 1);
        }

        
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

    public void pauseOnOff(){
        pausing = !pausing;
        GameManager.isPaused = !GameManager.isPaused;

        if(pausing){
            btnPause.GetComponent<Image>().sprite = toResume;
        }else{
            btnPause.GetComponent<Image>().sprite = toPause;
        }
        
    }

    public void soundOnOff()
    {
        muted = !muted;
        GameManager.isMute = !GameManager.isMute;

        if (muted)
        {
            btnSound.GetComponent<Image>().sprite = bsOff;
        }
        else
        {
            btnSound.GetComponent<Image>().sprite = bsOn;
        }

    }

    public void quitGame(){
        SceneManager.LoadScene(sceneName: "startScene");
    }

    public void settingTrigger()
    {
        settingsBar.gameObject.SetActive(true);

    }

    public void endtostart(){
        SceneManager.LoadScene(sceneName: "startScene");
    }
}
