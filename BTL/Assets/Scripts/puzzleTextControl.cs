using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleTextControl : MonoBehaviour {
    [Header("Initial Objects")]
    //GameManager
    public GameObject GM;

    [Header("UI")]
    //UI
    public GameObject inventory;
    public GameObject puzHintObj; //hint
    public Button shapeBtn;
    public Sprite btnImg;
    //Date
    public Image dateTime;

    [Header("Next Properties")]
    public int moduleN = 0;
    public int sentenceN = 0;
    int nextIndex = 0;
    public GameObject[] nextObj;

    [Header("This Properties")]
    //current 
    public int moduleC = 0;
    public int sentenceC = 0;
    //Properties
    public bool isTriggered = false;
    public bool isTheLast = false;
    //external assets
    public GameObject thisModel;
    public Texture thisCookie;
    //time Set
    public int curYear = 0000;
    public int curMonth = 00;
    public int curDay = 00;

    [Header("Item Drop")]
    //item drop
    public bool haveItemDrop = false;
    public int itemNum = 0;//the overall number
    public Image itemDropRmd;
    public Sprite itemSprite;
    public string itemText;
    public bool itemDropped = false;

    [Header("Other Effects")]
    //other trigger
    public bool haveOtherTrigger = false;
    public GameObject otherTrigger;




    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPaused)
        {

            if (GameManager.checkedSth && GameManager.shapeM == moduleC && GameManager.shapeS == sentenceC)
            {
                GameManager.checkedSth = false;
                changeBtn();
            }

            if (GameManager.puzSolved && GameManager.curModule == moduleC && GameManager.curSentence == sentenceC)
            {
                GameManager.puzSolved = false;
                hideHint();
                showNext();
                transform.GetChild(1).gameObject.GetComponent<lightUpText>().turnedOn = true;
                //if have item drop, drop item
                if (haveItemDrop && !itemDropped)
                {
                    //drop item UI
                    itemDropRmd.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemSprite;
                    itemDropRmd.gameObject.SetActive(true);
                    //add to bar
                    //1.show slot
                    for (int i = 0; i < 12; i++)
                    {
                        if (!inventory.transform.GetChild(i).GetComponent<slotControl>().isTriggered)
                        {
                            inventory.transform.GetChild(i).GetComponent<slotControl>().changeItemImg(itemSprite, itemText, itemNum);
                            inventory.transform.GetChild(i).GetComponent<slotControl>().turnOn();
                            break;
                        }
                    }
                    itemDropped = true;


                }
            }
        }
    }

    public void changeTime(){
        dateTime.transform.GetChild(0).GetComponent<Text>().text = curYear.ToString("0000");
        dateTime.transform.GetChild(1).GetComponent<Text>().text = curMonth.ToString("00");
        dateTime.transform.GetChild(2).GetComponent<Text>().text = curDay.ToString("00");
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
