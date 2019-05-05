﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //gameplay control
    [Header("Game Flow Control")]
    public int gameLoop = 0;
    static public bool isPaused = false;
    static public int collectTotalNum = 3;

    //key names:
    static public string gloopKey = "myGameLoop";
    static public string curModuleKey = "myCurrentModule";
    static public string curSentenceKey = "myCurrentSentence";
    static public string[] collectItemKey = new string[12] {"cItm0", "cItm1", "cItm2", "cItm3", "cItm4", "cItm5", "cItm6", "cItm7", "cItm8", "cItm9", "cItm10", "cItm11"};
    static public string[] moduleProgressKey = new string[17] { "module0", "module1", "module2", "module4", "module5", "module6", "module7", "module8", "module9", "module10", "module11", "module12", "module12", "module13", "module14", "module15", "module16"};
    static public string goalUpdateKey = "goalNeedUpdate";
    static public string[] moduleTriggerTimes = new string[17] { "trigger0", "trigger1", "trigger2", "trigger3", "trigger4", "trigger5", "trigger6", "trigger7", "trigger8", "trigger9", "trigger10", "trigger11", "trigger12", "trigger13", "trigger14", "trigger15", "trigger16" };

    //1 = true; 0 = false;

    [Header("Game Objects")]
    //objects
    public GameObject MainCamObj;
    public GameObject player;
    //static public bool[] itemCollectionCheck = new bool[12]{false, false, false, false, false, false, false, false, false, false, false, false};

    [Header("Properties Preset")]
    //game level preset
    static public int[] moduleProgress = new int[17] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    static public int[] moduleProgressUB = new int[17] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
    static public int[,] ModuleSentence = new int[10, 10]; //save the current progress
    static public int[,] ModuleSentenceUB = new int[10, 10]; //save the upper bound
    static public int[] NumOfSenInModule = new int[10] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }; //useless??
    
    [Header("Current Game Status")]
    //current level
    //static public int currentPuzzle = -1;
    static public int curModule = 0;
    static public int curSentence = 0;
    //current shape
    static public int curShapeM = 0;
    static public int curShapeS = 0;

    //for checking current
    bool firstTouch = true;
    static public bool puzSolved = false;
    static public bool onPuz = false;
    //check shape
    static public bool checkedSth = false;
    static public int shapeM = 0;
    static public int shapeS = 0;

    [Header("Sound Control")]
    //Sound
    public AudioSource mainBGM;
    public AudioSource SoundEffectSrc;
    public AudioClip s_getShape;
    public AudioClip s_nextLine;
    public AudioClip s_locked;
    static public bool isMute = false;

    [Header("UI Control")]
    public Sprite[] itemCollectSp = new Sprite[12];
    public string[] thisItemText = new string[12]{"记忆通行符：\n这是记忆大陆唯一的通行证。\n请带上它上路，收集另外<b><color=red>" + GameManager.collectTotalNum + "个</color></b>记忆碎片，走向无限光明的终点。离开时，系统会将它和记忆碎片一并回收。祝你好运。",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",
        "二：\n我是第二个！",};
    public Image inventory;


	private void Awake()
	{
        ModuleSentenceUB[1, 0] = 1;
        ModuleSentence[1, 0] = 0;

        ModuleSentenceUB[1, 1] = 1;
        ModuleSentence[1, 1] = 0;

        ModuleSentenceUB[1, 2] = 1;
        ModuleSentence[1, 2] = 0;
           
	}

	// Use this for initialization
	void Start () {
        SoundEffectSrc = GetComponent<AudioSource>();
        initializeGame();
	}
	


    void initializeGame()
    {
        //set game loop
        if (!PlayerPrefs.HasKey(gloopKey))
        {
            PlayerPrefs.SetInt(gloopKey, 0);
        }
        else
        {
            gameLoop = PlayerPrefs.GetInt(gloopKey, 0);
            print("[loacl storage] Game loop overall loaded");
        }

        if (!PlayerPrefs.HasKey(goalUpdateKey))
        {
            PlayerPrefs.SetInt(goalUpdateKey, 0);
            print("[loacl storage] First goal!!");
        }

        //set item dropped
        for (int i = 0; i < 12;i++){
            if (PlayerPrefs.HasKey(collectItemKey[i])){
                for (int j = 0; j < 12; j++)
                {
                    if (!inventory.transform.GetChild(i).GetComponent<slotControl>().isTriggered)
                    {
                        inventory.transform.GetChild(i).GetComponent<slotControl>().changeItemImg(itemCollectSp[i], thisItemText[i], i);
                        inventory.transform.GetChild(i).GetComponent<slotControl>().turnOn();
                        break;
                    }
                }
                print("[loacl storage] loaded item: item" + i);
            }
        }

        //moduleProgressKey
        for (int i = 0; i < 17; i++)
        {
            if (PlayerPrefs.HasKey(moduleProgressKey[i]))
            {
                moduleProgress[i] = PlayerPrefs.GetInt(moduleProgressKey[i], 0);
                print("[loacl storage] loaded module progress in M" + i + " = " + moduleProgress[i]);
            }
        }
    }


	// Update is called once per fr ame
	void Update () {

        if (!isPaused)
        {
            mainBGM.UnPause();
            if (firstTouch && (Input.touchCount >= 2 || Input.GetKeyDown(KeyCode.Return)))
            {
                firstTouch = false;
                myLight.inControl = false;
                //1. show button
                for (int i = 0; i <= curSentence; i++)
                {
                    print(i);
                    if (checkRegion(curModule, i))
                    {
                        checkedSth = true;
                        shapeM = curModule;
                        shapeS = i;
                        SoundEffectSrc.PlayOneShot(s_getShape);

                    }
                }


                //2.check puzzle if it is a puzzle
                if (onPuz) //puz mod
                {
                    print("checking");
                    if (checkRegion(curModule, curSentence))
                    {
                        print("Puzzle Solved!!!!!");
                        puzSolved = true;

                    }
                }



            }


            //if hands up, 
            if (Input.GetMouseButtonUp(0))
            {
                firstTouch = true;
                myLight.inControl = true;
            }
        }

        if(isPaused){
            mainBGM.Pause();
        }
	}





    bool checkRegion(int cm,int cs){       
        float charX = GetPuzzleRegion(cm, cs)[0];
        float charZ = GetPuzzleRegion(cm, cs)[1];
        float CamAngleX = GetPuzzleRegion(cm, cs)[2];
        float CamAngleY = GetPuzzleRegion(cm, cs)[3];
        float ChrDeltaX = GetPuzzleRegion(cm, cs)[4];
        float ChrDeltaZ = GetPuzzleRegion(cm, cs)[5];
        float CamDeltaX = GetPuzzleRegion(cm, cs)[6];
        float CamDeltaY = GetPuzzleRegion(cm, cs)[7];
        int sm = GetShapeUsed(cm, cs)[0];
        int ss = GetShapeUsed(cm, cs)[1];

        float curAngley;
        if(MainCamObj.transform.rotation.eulerAngles.y>180){
            curAngley = MainCamObj.transform.rotation.eulerAngles.y - 360;
        }else{
            curAngley = MainCamObj.transform.rotation.eulerAngles.y;
        }
        print(Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX);
        print(Mathf.Abs((float)(player.transform.position.z - charZ)) <= ChrDeltaZ);
        print(Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.x - CamAngleX)) <= CamDeltaX);
        print(Mathf.Abs((float)(curAngley - CamAngleY)) <= CamDeltaY);
        //print(Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.y - CamAngleY)));
        print(curShapeM == sm);
        print(curShapeS == ss);

        if(Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX
           && Mathf.Abs((float)(player.transform.position.z - charZ)) <= ChrDeltaZ
           && Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.x - CamAngleX)) <= CamDeltaX
           && Mathf.Abs((float)(curAngley - CamAngleY)) <= CamDeltaY
           && curShapeM == sm && curShapeS == ss){
            return true;
        }
        else{
            return false;
        }
    }

    float[] GetPuzzleRegion(int m, int s){
        float[] bounds = new float[9];
        //bounds[0] CharX
        //bounds[1] CharZ
        //bounds[2] CamAngleX
        //bounds[3] CamAngleY
        //bounds[4] ChrDeltaX
        //bounds[5] ChrDeltaZ
        //bounds[6] CamDeltaX
        //bounds[7] CamDeltaY
        if(m == 1){
            if (s == 1)
            {
                bounds[0] = 17f;
                bounds[1] = 74f;
                bounds[2] = 27f;
                bounds[3] = 0f;
                bounds[4] = 1.5f;
                bounds[5] = 3f;
                bounds[6] = 3f;
                bounds[7] = 3f;
            }
            else if (s == 2)
            {
                bounds[0] = 43.8f;
                bounds[1] = 113.8f;
                bounds[2] = 25f;
                bounds[3] = 0f;
                bounds[4] = 3f;
                bounds[5] = 3f;
                bounds[6] = 1f;
                bounds[7] = 3f;
            }
        }
        return bounds;
    }




    int[] GetShapeUsed(int m, int s)
    {
        int[] shapeMS = new int[2];
        //bounds[0] CharX
        //bounds[1] CharZ

        if (m == 1)
        {
            if (s == 1)
            {
                shapeMS[0] = 0;
                shapeMS[1] = 0;
            }else if(s == 2){
                shapeMS[0] = 1;
                shapeMS[1] = 1;
            }
        }
        return shapeMS;
    }

    public void playNextLineSound(){
        SoundEffectSrc.PlayOneShot(s_nextLine);
    }

    public void playLockSound()
    {
        SoundEffectSrc.PlayOneShot(s_locked);
    }
}
