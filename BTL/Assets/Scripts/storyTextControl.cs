using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storyTextControl : MonoBehaviour {
    [Header("Initial Objects")]
    //GameManager
    public GameObject GM;

    [Header("Next Properties")]
    public int moduleN = 0;
    public int sentenceN = 0;
    int nextIndex = 0;
    public GameObject[] nextObj;

    [Header("This Properties")]
    //current
    public int moduleC = 0;
    public int sentenceC = 0;
    public bool isTriggered = false;
    public bool isTheLast = false;

    [Header("UI")]
    //UI 
    public Image dateTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showNext(){
        GameManager.curModule = moduleC;
        GameManager.curSentence = sentenceC;
        if (!isTheLast)
        {
            nextIndex = GameManager.ModuleSentence[moduleN, sentenceN];
            nextObj[nextIndex].SetActive(true);
            GM.GetComponent<GameManager>().playNextLineSound();
        }
    }
}
