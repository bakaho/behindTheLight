using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //gameplay control
    [Header("Game Flow Control")]
    static public int gameLoop = 0;
    static public bool isPaused = false;
    static public int collectTotalNum = 3;

    //key names:
    static public string guidedKey = "guided";
    static public string gloopKey = "myGameLoop";
    static public string curModuleKey = "myCurrentModule";
    static public string curSentenceKey = "myCurrentSentence";
    static public string curGoalNumKey = "curGoalNum";
    static public string nextGoalNumKey = "nextGoalNum";
    static public string inModuleKey = "isInModule"; //equal to a module number
    static public string inRoundKey = "isInRound";
    static public string goodBadKey = "goodBad"; //0 = bad; 1 = good
    static public string[] collectItemKey = new string[12] {"cItm0", "cItm1", "cItm2", "cItm3", "cItm4", "cItm5", "cItm6", "cItm7", "cItm8", "cItm9", "cItm10", "cItm11"};
    static public string[] moduleProgressKey = new string[8] { "module0", "module1", "module2", "module3", "module4", "module5", "module6", "module7"};
    static public string goalUpdateKey = "goalNeedUpdate";
    static public string[] moduleTriggerTimes = new string[8] { "trigger0", "trigger1", "trigger2", "trigger3", "trigger4", "trigger5", "trigger6", "module7"};

    //1 = true; 0 = false;

    [Header("Game Objects")]
    //objects
    public GameObject MainCamObj;
    public GameObject player;
    //static public bool[] itemCollectionCheck = new bool[12]{false, false, false, false, false, false, false, false, false, false, false, false};

    [Header("Properties Preset")]
    //game level preset
    static public int[] moduleProgress = new int[8] {0, 0, 0, 0, 0, 0, 0, 0};
    static public int[] moduleProgressUB = new int[8] {1, 1, 1, 1, 1, 1, 1, 1};
    static public int[,] ModuleSentence = new int[10, 10]; //save the current progress
    static public int[,] ModuleSentenceUB = new int[10, 10]; //save the upper bound
    static public int[] NumOfSenInModule = new int[10] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }; //useless??
    //how many piece can be found after unlocking this level
    static public int[,] mAdd = new int[,] {
        //{can be unlocked at lv1, lv2}
        //total num should be 8
        {0, 0}, {0, 0}, {1, 0}, {0, 1}, 
        {1, 1}, {0, 0}, {1, 1}, 
        {1, 1}
        //or set d and f to item drop
    };

    
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
    public collectCheckerControl checker;
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
        PlayerPrefs.DeleteAll();
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


        //set cur goal
        if (PlayerPrefs.GetInt(inRoundKey, 0) == 0)
        {
            PlayerPrefs.SetInt(curGoalNumKey, PlayerPrefs.GetInt(nextGoalNumKey, 4));
            print("[local storage] current goal is set to" + PlayerPrefs.GetInt(curGoalNumKey));
        }
        collectTotalNum = PlayerPrefs.GetInt(curGoalNumKey) - 1;


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
                checker.lightOn();
                print("[loacl storage] loaded item: item" + i);
            }
        }

        //set module and sentence
        if(PlayerPrefs.HasKey(curModuleKey) && PlayerPrefs.HasKey(curSentenceKey)){
            curModule = PlayerPrefs.GetInt(curModuleKey);
            curSentence = PlayerPrefs.GetInt(curSentenceKey);
            print("[loacl storage] Reloaded M = " + curModule + " S = " + curSentence);
        }


        //moduleProgressKey
        for (int i = 0; i < 8; i++)
        {
            if (PlayerPrefs.HasKey(moduleProgressKey[i]))
            {
                moduleProgress[i] = PlayerPrefs.GetInt(moduleProgressKey[i], 0);
                print("[loacl storage] loaded module progress in M" + i + " = " + moduleProgress[i]);
            }
        }

        //retrieve before
        if (PlayerPrefs.HasKey(inRoundKey) && PlayerPrefs.GetInt(inRoundKey) == 1)
        {
            bool alreadyFindCur = false;
            //puzzles
            GameObject[] pTxts = GameObject.FindGameObjectsWithTag("puzzleText");
            foreach(GameObject pt in pTxts){
                if(!alreadyFindCur && pt.GetComponent<puzzleTextControl>().moduleC== curModule && pt.GetComponent<puzzleTextControl>().sentenceC == curSentence){
                    player.transform.position = pt.transform.position - new Vector3(0, 0, 54);
                    //MainCamObj.transform.position = player.transform.position + new Vector3(0, 65, -68);
                }
                //pt.SetActive(true);
                if(PlayerPrefs.HasKey("M" + pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC) &&
                    PlayerPrefs.GetInt("M"+pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC) != 0){
                    pt.GetComponent<BoxCollider>().enabled = true;
                    pt.transform.GetChild(0).gameObject.SetActive(true);
                    pt.transform.GetChild(1).gameObject.SetActive(true);
                    if (PlayerPrefs.GetInt("M" + pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC) != 2)
                    {
                        pt.transform.GetChild(1).GetComponent<Light>().intensity = 60;
                        pt.transform.GetComponent<puzzleTextControl>().isTriggered = true;
                    }
                }

            }

            //stories
            GameObject[] sTxts = GameObject.FindGameObjectsWithTag("storyText");
            foreach (GameObject st in sTxts)
            {
                if (!alreadyFindCur && st.GetComponent<storyTextControl>().moduleC == curModule && st.GetComponent<storyTextControl>().sentenceC == curSentence)
                {
                    player.transform.position = st.transform.position - new Vector3(0, 0, 54);
                    //MainCamObj.transform.position = player.transform.position + new Vector3(0, 65, -68);
                }

                if (PlayerPrefs.HasKey("M" + st.GetComponent<storyTextControl>().moduleC + "S" + st.GetComponent<storyTextControl>().sentenceC) &&
                    PlayerPrefs.GetInt("M" + st.GetComponent<storyTextControl>().moduleC + "S" + st.GetComponent<storyTextControl>().sentenceC) != 0)
                {
                    //st.SetActive(true);
                    st.GetComponent<BoxCollider>().enabled = true;
                    st.transform.GetChild(0).gameObject.SetActive(true);
                    st.transform.GetChild(1).gameObject.SetActive(true);
                    if (PlayerPrefs.GetInt("M" + st.GetComponent<storyTextControl>().moduleC + "S" + st.GetComponent<storyTextControl>().sentenceC) != 2)
                    {
                        st.transform.GetChild(1).GetComponent<Light>().intensity = 60;
                        st.transform.GetComponent<storyTextControl>().isTriggered = true;
                    }
                }
            }
        }

        //set is finished
        GameObject[] pTxtF = GameObject.FindGameObjectsWithTag("puzzleText");
        foreach (GameObject pt in pTxtF)
        {

            //pt.SetActive(true);
            if (PlayerPrefs.HasKey("M" + pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC + "Finish") &&
                PlayerPrefs.GetInt("M" + pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC + "Finish") == 1)
            {
                pt.transform.GetComponent<puzzleTextControl>().isFinished = true;
            }
        }



        //set in round
        PlayerPrefs.SetInt(inRoundKey, 1);
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

    public void finishRound(){
        GameObject[] pTxts = GameObject.FindGameObjectsWithTag("puzzleText");
        foreach (GameObject pt in pTxts)
        {
            //sabe finished clear triggered
            if(pt.GetComponent<puzzleTextControl>().isTriggered){
                PlayerPrefs.SetInt("M" + pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC + "Finish", 1);
                print("[local storage] M" + pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC + " is saved as Finished Forever");

                PlayerPrefs.SetInt("M" + pt.GetComponent<puzzleTextControl>().moduleC + "S" + pt.GetComponent<puzzleTextControl>().sentenceC, 0);
            }


        }
        PlayerPrefs.SetInt(inRoundKey, 0);
    }
}
