using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleTextControl : MonoBehaviour {
    public bool isTriggered = false;
    public bool isTheLast = false;
    public GameObject[] nextObj;
    public GameObject puzHintObj;
    public int moduleN = 0;
    public int sentenceN = 0;

    //current 
    public int moduleC = 0;
    public int sentenceC = 0;
    int nextIndex = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.puzSolved && GameManager.curModule == moduleC && GameManager.curSentence == sentenceC){
            GameManager.puzSolved = false;
            hideHint();
            //showNext();
        }
    }

    //show puz

    public void showHint(){
        //set puzzle
        GameManager.curModule = moduleC;
        GameManager.curSentence = sentenceC;
        //show hint
        puzHintObj.SetActive(true);
    }

    public void hideHint(){
        puzHintObj.SetActive(false);
        GameManager.curModule = -1;
        GameManager.curSentence = -1;
    }

    public void showNext()
    {
        nextIndex = GameManager.ModuleSentence[moduleN, sentenceN];
        nextObj[nextIndex].SetActive(true);
    }
}
