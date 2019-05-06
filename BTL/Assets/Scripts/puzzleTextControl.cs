using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Linq;
using PathCreation;

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
    public bool testMode = false;
    public int moduleC = 0;
    public int sentenceC = 0;
    //Properties
    public bool isTriggered = false;
    public bool isFinished = false;
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
    public GameObject borderToClose;

    [Header("Path Selector")]
    public GameObject[] aiDot = new GameObject[4];
    public PathCreator[] pathForward;
    int totalLevel = 7;


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
        //set player pref
        PlayerPrefs.SetInt(GameManager.curModuleKey,moduleC);
        PlayerPrefs.SetInt(GameManager.curSentenceKey, sentenceC);
        print("[loacl storage] Module saved: " + PlayerPrefs.GetInt(GameManager.curModuleKey) + ", Sentence saved: " + PlayerPrefs.GetInt(GameManager.curSentenceKey));
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


        if (haveOtherTrigger)
        {
            otherTrigger.gameObject.SetActive(true);
        }

        //show next line
        if (!isTheLast)
        {
            //nextIndex = GameManager.ModuleSentence[moduleN, sentenceN];
            nextIndex = GameManager.moduleProgress[moduleC];
            //nextObj[nextIndex].SetActive(true);
            nextObj[nextIndex].GetComponent<BoxCollider>().enabled = true;
            nextObj[nextIndex].transform.GetChild(0).gameObject.SetActive(true);
            nextObj[nextIndex].transform.GetChild(1).gameObject.SetActive(true);

            GM.GetComponent<GameManager>().playNextLineSound();

        }else if(!testMode){
            //is the real last
            //module loop +1
            if (PlayerPrefs.GetInt(GameManager.moduleProgressKey[moduleC], 0) < GameManager.moduleProgressUB[moduleC])
            {
                PlayerPrefs.SetInt(GameManager.moduleProgressKey[moduleC], GameManager.moduleProgress[moduleC] + 1);
            }
            print("[loacl storage] Module Upgraded for M" + moduleC + ", it will be level" + PlayerPrefs.GetInt(GameManager.moduleProgressKey[moduleC], 0) + " in the next round");

            borderToClose.SetActive(false);
            choosePath();
            
        }
        PlayerPrefs.SetInt("M" + moduleC + "S" + sentenceC,1);
        print("[loacl storage] M" + moduleC + "S" + sentenceC + " is saved as triggered");
        PlayerPrefs.SetInt("M" + moduleN + "S" + sentenceN, 2);
        print("[loacl storage] M" + moduleN + "S" + sentenceN + " is saved as the next");
    }

    public void finishedShowNext(){
        //set puzzle
        GameManager.onPuz = true;
        GameManager.curModule = moduleC;
        GameManager.curSentence = sentenceC;
        //set player pref
        PlayerPrefs.SetInt(GameManager.curModuleKey, moduleC);
        PlayerPrefs.SetInt(GameManager.curSentenceKey, sentenceC);
        print("[loacl storage] Module saved: " + PlayerPrefs.GetInt(GameManager.curModuleKey) + ", Sentence saved: " + PlayerPrefs.GetInt(GameManager.curSentenceKey));
        //show next
        showNext();
    }

    public void choosePath()
    {
        List<int> counts = new List<int>();

        if (pathForward.Length > 4)
        {
            //get server
            print("[Online Database] Retrieving");

            for (int i = moduleC + 1; i < totalLevel; i++)
            {
                RestClient.Get<Waypoint>("https://behindthelight-f424f.firebaseio.com/" + i + ".json").Catch(onRejected: response =>
                {
                    print("[Online Database] No internet connection, added from local storage");
                    counts.Add(PlayerPrefs.GetInt(GameManager.moduleTriggerTimes[i], 1));
                    if (i == totalLevel)
                    {
                        var sorted = counts.Select((x, k) => new KeyValuePair<int, int>(x, k)).OrderBy(x => x.Key).ToList();

                        List<int> B = sorted.Select(x => x.Key).ToList();
                        List<int> idx = sorted.Select(x => x.Value).ToList();

                        for (int j = 0; j < 4; j++)
                        {
                            aiDot[j].GetComponent<pathFixed>().pathCreator = pathForward[idx[j]];
                            aiDot[j].GetComponent<pathFixed>().resetDistanceOut();
                        }
                    }
                });

                RestClient.Get<Waypoint>("https://behindthelight-f424f.firebaseio.com/" + i + ".json").Then(onResolved: response =>
                {
                    print("[Online Database] Retrieved from online database");
                    counts.Add(response.times);
                    if (i == totalLevel)
                    {
                        var sorted = counts.Select((x, k) => new KeyValuePair<int, int>(x, k)).OrderBy(x => x.Key).ToList();

                        List<int> B = sorted.Select(x => x.Key).ToList();
                        List<int> idx = sorted.Select(x => x.Value).ToList();

                        for (int j = 0; j < 4; j++)
                        {
                            aiDot[j].GetComponent<pathFixed>().pathCreator = pathForward[idx[j]];
                            aiDot[j].GetComponent<pathFixed>().resetDistanceOut();
                        }
                    }
                });
            }

            print("[Online Database] Finished");


        }
        else
        {
            for (int j = 0; j < 4; j++)
            {
                aiDot[j].GetComponent<pathFixed>().pathCreator = pathForward[j];
                aiDot[j].GetComponent<pathFixed>().resetDistanceOut();
            }
        }
    }


}
