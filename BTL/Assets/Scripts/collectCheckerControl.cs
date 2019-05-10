using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectCheckerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int i = 0; i < PlayerPrefs.GetInt(GameManager.curGoalNumKey); i++){
            print("PlayerPrefs.GetInt(GameManager.curGoalNumKey" + PlayerPrefs.GetInt(GameManager.curGoalNumKey));
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void lightOn(){
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (!this.transform.GetChild(i).GetComponent<checkerDotControl>().isOn)
            {
                this.transform.GetChild(i).GetComponent<checkerDotControl>().setOn();
                break;
            }
        }
    }

    public bool checkPassed(){
        for (int i = 0; i < PlayerPrefs.GetInt(GameManager.curGoalNumKey,4); i++)
        {
            if (!this.transform.GetChild(i).GetComponent<checkerDotControl>().isOn)
            {
                return false;
            }
        }
        return true;
    }
}
