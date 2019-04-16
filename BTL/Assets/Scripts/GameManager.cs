using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //objects
    public GameObject MainCamObj;
    public GameObject player;
    
    //current level
    //static public int currentPuzzle = -1;
    static public int curModule = 0;
    static public int curSentence = 0;

    //game level preset
    static public int[,] ModuleSentence = new int[10,10];
    static public int[,] ModuleSentenceUB = new int[10, 10];

    //for checking current
    bool firstTouch = true;
    static public bool puzSolved = false;


	private void Awake()
	{
        ModuleSentenceUB[1, 0] = 1;
        ModuleSentence[1, 0] = 0;

        ModuleSentenceUB[1, 1] = 1;
        ModuleSentence[1, 1] = 0;
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


            //2.check puzzle if it is a puzzle
            if (curModule != -1 && curSentence != -1) //puz mod
            {
                if(checkPuz()){
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

    bool checkPuz(){
        print("checking");
        float charX = GetPuzzleRegion(curModule,curSentence)[0];
        float charZ = GetPuzzleRegion(curModule, curSentence)[1];
        float CamAngleX = GetPuzzleRegion(curModule, curSentence)[2];
        float CamAngleY = GetPuzzleRegion(curModule, curSentence)[3];
        float ChrDeltaX = GetPuzzleRegion(curModule, curSentence)[4];
        float ChrDeltaZ = GetPuzzleRegion(curModule, curSentence)[5];
        float CamDeltaX = GetPuzzleRegion(curModule, curSentence)[6];
        float CamDeltaY = GetPuzzleRegion(curModule, curSentence)[7];
        print(Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX);
        print(Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX);
        print(Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.x - CamAngleX)) < CamDeltaX);
        print(Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.y - CamAngleY)) < CamDeltaY);

        if(Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX
           && Mathf.Abs((float)(player.transform.position.x - charX)) <= ChrDeltaX
           && Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.x - CamAngleX)) < CamDeltaX
           && Mathf.Abs((float)(MainCamObj.transform.rotation.eulerAngles.y - CamAngleY)) < CamDeltaY){
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
