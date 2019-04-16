using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyTextControl : MonoBehaviour {
    //GameManager
    public GameObject GM;

    public bool isTriggered = false;
    public bool isTheLast = false;
    public GameObject[] nextObj;
    public int moduleN = 0;
    public int sentenceN = 0;
    //current
    public int moduleC = 0;
    public int sentenceC = 0;
    int nextIndex = 0;

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
