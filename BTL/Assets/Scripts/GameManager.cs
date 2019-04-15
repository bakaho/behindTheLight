using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //game level
    static public int currentPuzzle = -1;
    static public int[,] ModuleSentence = new int[10,10];
    static public int[,] ModuleSentenceUB = new int[10, 10];

	// Use this for initialization
	void Start () {
        ModuleSentenceUB[1,0] = 1;
        ModuleSentence[1,0] = 0;
        ModuleSentenceUB[1, 1] = 1;
        ModuleSentence[1, 1] = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
