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
    public AudioSource Read;

    [Header("UI")]
    //UI
    public GameObject inventory;
    public GameObject puzHintObj; //hint
    public Button shapeBtn;
    public Sprite[] btnImg;
    //Date
    public Image dateTime;

    [Header("Next Properties")]
    public bool nextIsPuz = false;
    public int moduleN = 0;
    public int sentenceN = 0;
    public int nextIndex = 0;
    public GameObject[] nextObj;

    [Header("This Properties")]
    //current 
    public bool testMode = false;
    public int moduleC = 0;
    public int sentenceC = 0;
    public int indexC = 0;
    //Properties
    public bool isTriggered = false;
    public bool isFinished = false;
    public bool isTheLast = false;
    public bool isTheFirst = false;
    public int myMood = 0;
    //external assets
    public GameObject thisModel;
    public Texture[] thisCookie;//change
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
    public collectCheckerControl checker;

    [Header("Other Effects")]
    //other trigger
    public bool haveOtherTrigger = false;
    public GameObject otherTrigger;
    public GameObject borderToClose;

    [Header("Sound")]
    public AudioClip voiceOver;

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

            //shapeI ==
            if (GameManager.checkedSth && GameManager.shapeM == moduleC && GameManager.shapeS == sentenceC)
            {
                GameManager.checkedSth = false;
                //add a i to set which
                //changeBtn(shapeI)
                changeBtn(GameManager.shapeI);
            }

            // && shape index = thisImg.length-1
            if (GameManager.puzSolved && GameManager.curModule == moduleC && GameManager.curSentence == sentenceC && GameManager.curIndex == indexC)
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
                    //2.light dot
                    checker.lightOn();
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
    void changeBtn(int i){
        shapeBtn.gameObject.SetActive(true);
        shapeBtn.GetComponent<Image>().sprite = btnImg[i];
        buttonControl.lightCookie = thisCookie[i];
    }

    //set

    public void showHint(){
        
        if(isTheFirst){
            PlayerPrefs.SetInt("finalMood", myMood);
        }
        //firstTime count
        PlayerPrefs.SetInt(GameManager.unlockedLineKey, PlayerPrefs.GetInt(GameManager.unlockedLineKey, 0) + 1);
        //set puzzle
        GameManager.onPuz = true;
        GameManager.curModule = moduleC;
        GameManager.curSentence = sentenceC;
        GameManager.curIndex = indexC;
        //set player pref
        PlayerPrefs.SetInt(GameManager.curModuleKey,moduleC);
        PlayerPrefs.SetInt(GameManager.curSentenceKey, sentenceC);
        PlayerPrefs.SetInt(GameManager.curIndexKey, indexC);
        print("[loacl storage] Module saved: " + PlayerPrefs.GetInt(GameManager.curModuleKey) + ", Sentence saved: " + PlayerPrefs.GetInt(GameManager.curSentenceKey) + ", Index saved: " + PlayerPrefs.GetInt(GameManager.curIndexKey));
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
        if (!GameManager.isMute)
        {
            Read.Stop();
            Read.PlayOneShot(voiceOver);
        }
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
            int gb = PlayerPrefs.GetInt(GameManager.goodBadKey, 0);
            nextObj[2 * nextIndex + gb].GetComponent<BoxCollider>().enabled = true;
            nextObj[2 * nextIndex + gb].transform.GetChild(0).gameObject.SetActive(true);
            nextObj[2 * nextIndex + gb].transform.GetChild(1).gameObject.SetActive(true);

            GM.GetComponent<GameManager>().playNextLineSound();

        }else if(!testMode){
            //is the real last
            updateGoal();
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
        if (!GameManager.isMute)
        {
            Read.Stop();
            Read.PlayOneShot(voiceOver);
        }
        if (isTheFirst)
        {
            PlayerPrefs.SetInt("finalMood", myMood);
        }
        //set puzzle
        GameManager.onPuz = true;
        GameManager.curModule = moduleC;
        GameManager.curSentence = sentenceC;
        GameManager.curIndex = indexC;
        //set player pref
        PlayerPrefs.SetInt(GameManager.curModuleKey, moduleC);
        PlayerPrefs.SetInt(GameManager.curSentenceKey, sentenceC);
        PlayerPrefs.SetInt(GameManager.curIndexKey, indexC);
        print("[loacl storage] Module saved: " + PlayerPrefs.GetInt(GameManager.curModuleKey) + ", Sentence saved: " + PlayerPrefs.GetInt(GameManager.curSentenceKey) + ", Index saved: " + PlayerPrefs.GetInt(GameManager.curIndexKey));
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

    public void updateGoal(){
        PlayerPrefs.SetInt(GameManager.nextGoalNumKey,PlayerPrefs.GetInt(GameManager.nextGoalNumKey)+GameManager.mAdd[moduleC,GameManager.moduleProgress[moduleC]]);
    }


}
