using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotControl : MonoBehaviour {
    public bool isTriggered = false; //if an item in
    public GameObject panel;
    Sprite myPic;
    public string myTxt;
    public string text2;
    public int thisItemNum;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeItemImg(Sprite sp, string s, int num){
        this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = sp;
        myPic = sp;
        myTxt = s;
        text2 = s;
        thisItemNum = num;
        //GameManager.itemCollectionCheck[num] = true;
        if (!PlayerPrefs.HasKey(GameManager.collectItemKey[num]))
        {
            PlayerPrefs.SetInt(GameManager.collectItemKey[num], 1);
            print("[loacl storage] Item" + num + " collected!");
        }
    }
    public void turnOn(){
        //this.gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        isTriggered = true;
    }

    public void showPanel(){
        if(isTriggered){
            panel.gameObject.SetActive(true);
            panel.gameObject.transform.GetChild(1).GetComponent<Text>().text = text2;
            panel.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = myPic;
        }
    }

}
