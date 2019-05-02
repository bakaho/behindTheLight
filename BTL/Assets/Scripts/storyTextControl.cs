using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storyTextControl : MonoBehaviour {
    [Header("Initial Objects")]
    //GameManager
    public GameObject GM;

    [Header("UI")]
    //UI 
    public Image dateTime;

    [Header("Next Properties")]
    public int moduleN = 0;
    public int sentenceN = 0;
    int nextIndex = 0;
    public GameObject[] nextObj;

    [Header("This Properties")]
    //current
    public bool testMode = false;
    public int moduleC = 0;
    public int sentenceC = 0;
    public bool isTriggered = false;
    public bool isTheLast = false;
    //time Set
    public int curYear = 0000;
    public int curMonth = 00;
    public int curDay = 00;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeTime()
    {
        dateTime.transform.GetChild(0).GetComponent<Text>().text = curYear.ToString("0000");
        dateTime.transform.GetChild(1).GetComponent<Text>().text = curMonth.ToString("00");
        dateTime.transform.GetChild(2).GetComponent<Text>().text = curDay.ToString("00");
    }

    public void showNext(){
        GameManager.curModule = moduleC;
        GameManager.curSentence = sentenceC;
        if (!isTheLast)
        {
            //nextIndex = GameManager.ModuleSentence[moduleN, sentenceN];
            nextIndex = GameManager.moduleProgress[moduleC];
            nextObj[nextIndex].SetActive(true);
            GM.GetComponent<GameManager>().playNextLineSound();
        }else if (!testMode)
        {
            //is the real last
            //module loop +1
            PlayerPrefs.SetInt(GameManager.moduleProgressKey[moduleC], GameManager.moduleProgress[moduleC] + 1);
            print("[loacl storage] Module Upgraded for M" + moduleC + ", it will be level" + PlayerPrefs.GetInt(GameManager.moduleProgressKey[moduleC], 0) + " in the next round");

        }
    }
}
