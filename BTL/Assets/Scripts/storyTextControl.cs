using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyTextControl : MonoBehaviour {
    public bool isTriggered = false;
    public GameObject[] nextObj;
    public int moduleN = 0;
    public int sentenceN = 0;
    int nextIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showNext(){
        nextIndex = GameManager.ModuleSentence[moduleN,sentenceN];
        nextObj[nextIndex].SetActive(true);
    }
}
