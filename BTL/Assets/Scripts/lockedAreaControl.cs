using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lockedAreaControl : MonoBehaviour {
    public bool isLocked = true;
    public int myIndex = 0;

    public Image inventory;
    public Text unlockRmd;
    //self define length of the array
    public GameObject[] itemToBeUnlocked;
    public int[] itemNumbers;
    public Sprite[] itemAlreadyUnlocked;
    public int[] itemIsUnlocked; //0 = true; 1 = false
    public bool levelClear = false;

    public GameObject hiddenObj;

    [Header("Sound")]
    public AudioSource myAudio;
    public AudioClip unlockSound;


	// Use this for initialization
	void Start () {
        myAudio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!isLocked && !myAudio.isPlaying){
            levelClearMoveOn();
        }
		
	}

    public void checkUnlock(){
        //show the slots
        for (int i = 0; i < unlockRmd.transform.childCount; i++)
        {
            unlockRmd.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < 12; i++)
        {
            if (inventory.transform.GetChild(i).GetComponent<slotControl>().isTriggered)
            {
                for (int j = 0; j < itemToBeUnlocked.Length; j++)
                {
                    if(inventory.transform.GetChild(i).GetComponent<slotControl>().thisItemNum == itemNumbers[j]){
                        itemToBeUnlocked[j].GetComponent<Image>().sprite = itemAlreadyUnlocked[j];
                        itemIsUnlocked[j] = 0;
                    }
                }

            }else{
                break;
            }
        }

        for (int i = 0; i < itemToBeUnlocked.Length; i++)
        {
            itemToBeUnlocked[i].SetActive(true);
        }

        int addUpCheck = 0;
        for (int i = 0; i < itemToBeUnlocked.Length; i++){
            addUpCheck += itemIsUnlocked[i];
        }
        if(addUpCheck == 0){
            levelClear = true;
        }

        if(levelClear){
            isLocked = false;
            myAudio.PlayOneShot(unlockSound);
        }

    }

    void levelClearMoveOn(){
        this.gameObject.SetActive(false);
        hiddenObj.SetActive(true);
        PlayerPrefs.SetInt(GameManager.unlockKey[myIndex], 1);
    }
}
