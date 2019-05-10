using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startMenuManager : MonoBehaviour {
    [Header("Percentage Bar")]
    public Slider progressBar;

    [Header("Sound")]
    public AudioSource myBGM;
    public AudioClip[] mySound;

    [Header("Curtain")]
    public GameObject curtain;

    [Header("Start Button")]
    public Button startBtn;
    public Sprite spStart;
    public Sprite spResume;
    public Sprite spEnd;

    [Header("Help")]
    public GameObject helpCurtain;

	// Use this for initialization
	void Start () {
        if(PlayerPrefs.GetInt("FinalEnded", 0) == 1){
            startBtn.GetComponent<Image>().sprite = spEnd;
        }
        else if(PlayerPrefs.GetInt("isInRound",0) == 0){
            startBtn.GetComponent<Image>().sprite = spStart;
        }else{
            startBtn.GetComponent<Image>().sprite = spResume;
        }
        progressBar.value = PlayerPrefs.GetFloat("percentage", 0);
        float showPercentage = PlayerPrefs.GetFloat("percentage", 0) * 100;
        progressBar.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = Mathf.Round(showPercentage) + "%";
        myBGM.clip = mySound[PlayerPrefs.GetInt("finalMood", 2)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame(){
        curtain.SetActive(true);
    }

    public void showHelp(){
        helpCurtain.SetActive(true);
    }

    public void closeHelp()
    {
        helpCurtain.SetActive(false);

    }
}
