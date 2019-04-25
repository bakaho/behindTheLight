using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //gameplay control
    public int gameLoop = 0;


    //objects
    public GameObject MainCamObj;
    public GameObject player;
    
    //current level
    //static public int currentPuzzle = -1;
    static public int curModule = 0;
    static public int curSentence = 0;
    //current shape
    static public int curShapeM = 0;
    static public int curShapeS = 0;

    //game level preset
    static public int[,] ModuleSentence = new int[10,10]; //save the current progress
    static public int[,] ModuleSentenceUB = new int[10, 10]; //save the upper bound
    static public int[] NumOfSenInModule = new int[10]{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }; //useless??

    //for checking current
    bool firstTouch = true;
    static public bool puzSolved = false;
    static public bool onPuz = false;
    //check shape
    static public bool checkedSth = false;
    static public int shapeM = 0;
    static public int shapeS = 0;

    //Sound
    public AudioSource SoundEffectSrc;
    public AudioClip s_getShape;
    public AudioClip s_nextLine;
    public AudioClip s_locked;


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
	}
	
	// Update is called once per frame
	void Update () {

        if(firstTouch && (Input.touchCount >= 2 || Input.GetKeyDown(KeyCode.Return))){
            firstTouch = false;
            myLight.inControl = false;
            //1. show button
            for (int i = 0; i<=curSentence; i++){
                print(i);
                if(checkRegion(curModule, i)){
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
                if(checkRegion(curModule,curSentence)){
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
