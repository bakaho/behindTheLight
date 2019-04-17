using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleTextControl : MonoBehaviour {
    //GameManager
    public GameObject GM;

    //Properties
    public bool isTriggered = false;
    public bool isTheLast = false;
    public GameObject[] nextObj;
    public GameObject puzHintObj;
    public int moduleN = 0;
    public int sentenceN = 0;
    //item drop
    public bool haveItemDrop = false;
    public int itemNum = 0;
    public GameObject inventory;
    //other trigger
    public bool haveOtherTrigger = false;
    public GameObject otherTrigger;


    //model
    public GameObject thisModel;
    public Image itemDropRmd;
    public Sprite itemSprite;

    //button control
    public Sprite btnImg;
    public Button shapeBtn;
    public Texture thisCookie;


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
        if (GameManager.checkedSth && GameManager.shapeM == moduleC && GameManager.shapeS == sentenceC)
        {
            GameManager.checkedSth = false;
            changeBtn();
        }

        if(GameManager.puzSolved && GameManager.curModule == moduleC && GameManager.curSentence == sentenceC){
            GameManager.puzSolved = false;
            hideHint();
            showNext();
            transform.GetChild(1).gameObject.GetComponent<lightUpText>().turnedOn = true;
            //if have item drop, drop item
            if(haveItemDrop){
                //drop item UI
                itemDropRmd.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemSprite;
                itemDropRmd.gameObject.SetActive(true);
                //add to bar
                //1.show slot
                for (int i = 0; i < 12; i++){
                    if(!inventory.transform.GetChild(i).GetComponent<slotControl>().isTriggered){
                        inventory.transform.GetChild(i).GetComponent<slotControl>().changeItemImg(itemSprite);
                        inventory.transform.GetChild(i).GetComponent<slotControl>().turnOn();
                        break;
                    }
                }


            }
        }
    }

    //show puz
    void changeBtn(){
        shapeBtn.gameObject.SetActive(true);
        shapeBtn.GetComponent<Image>().sprite = btnImg;
        buttonControl.lightCookie = thisCookie;
    }

    //set

    public void showHint(){
        //set puzzle
        GameManager.onPuz = true;
        GameManager.curModule = moduleC;
        GameManager.curSentence = sentenceC;
        //show hint
        puzHintObj.SetActive(true);
    }

    public void hideHint(){
        puzHintObj.SetActive(false);
        GameManager.onPuz = false;
        //GameManager.curModule = -1;
        //GameManager.curSentence = -1;
    }

    public void showNext()
    {
        //show model
        thisModel.SetActive(true);

        //show next line
        if (!isTheLast)
        {
            nextIndex = GameManager.ModuleSentence[moduleN, sentenceN];
            nextObj[nextIndex].SetActive(true);
            GM.GetComponent<GameManager>().playNextLineSound();
            if(haveOtherTrigger){
                otherTrigger.gameObject.SetActive(true);
            }

        }
    }
}
