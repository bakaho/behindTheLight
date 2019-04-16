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

    //button control
    //public Image[,] btnImg = new Image[10, 10];


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
        
	}
	
	// Update is called once per frame
	void Update () {

        if(firstTouch && Input.touchCount >= 2){
            firstTouch = false;
            myLight.inControl = false;
            //1. show button
            for (int i = 0; i<=NumOfSenInModule[curModule]; i++){
                print(i);
                if(checkRegion(curModule, i)){
                    checkedSth = true;
                    shapeM = curModule;
                    shapeS = i;
                    //show button （check already on)
                    //assign button, if click button, change mylight shape(save where?) ,how to change?
                    //TODO: show button

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
        print(Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX);
        print(Mathf.Abs((float)(player.transform.position.z - charZ)) <= ChrDeltaZ);
        print(Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.x - CamAngleX)) <= CamDeltaX);
        print(Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.y - CamAngleY)) <= CamDeltaY);

        if(Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX
           && Mathf.Abs((float)(player.transform.position.z - charZ)) <= ChrDeltaZ
           && Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.x - CamAngleX)) <= CamDeltaX
           && Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.y - CamAngleY)) <= CamDeltaY){
            return true;
        }
        else{
            return false;
        }
    }

    float[] GetPuzzleRegion(int m, int s){
        float[] bounds = new float[8];
        //bounds[0] CharX
        //bounds[1] CharZ
        //bounds[2] CamAngleX
        //bounds[3] CamAngleY
        //bounds[4] ChrDeltaX
        //bounds[5] ChrDeltaZ
        //bounds[6] CamDeltaX
        //bounds[7] CamDeltaY
        if(m == 1){
            if(s == 1){
                bounds[0] = 17f;
                bounds[1] = 74f;
                bounds[2] = 27f;
                bounds[3] = 0f;
                bounds[4] = 1.5f;
                bounds[5] = 3f;
                bounds[6] = 3f;
                bounds[7] = 3f;
            }
        }
        return bounds;
    }
}
