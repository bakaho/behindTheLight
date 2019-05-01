using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lockedAreaControl : MonoBehaviour {
    public bool isLocked = true;

    public Image inventory;
    public Text unlockRmd;
    //self define length of the array
    public GameObject[] itemToBeUnlocked;
    public int[] itemNumbers;
    public Sprite[] itemAlreadyUnlocked;
    public int[] itemIsUnlocked; //0 = true; 1 = false
    public bool levelClear = false;

    public GameObject hiddenObj;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void checkUnlock(){
        //show the slots
        for (int i = 0; i < itemToBeUnlocked.Length; i++)
        {
            itemToBeUnlocked[i].SetActive(true);
        }

        for (int i = 0; i < 12; i++)
        {
            if (inventory.transform.GetChild(i).GetComponent<slotControl>().isTriggered)
            {
                for (int j = 0; j < itemToBeUnlocked.Length; j++)
                {
                    if(inventory.transform.GetChild(j).GetComponent<slotControl>().thisItemNum == itemNumbers[j]){
                        unlockRmd.transform.GetChild(j).GetComponent<Image>().sprite = itemAlreadyUnlocked[j];
                        itemIsUnlocked[j] = 0;
                    }
                }

            }else{
                break;
            }
        }

        int addUpCheck = 0;
        for (int i = 0; i < itemToBeUnlocked.Length; i++){
            addUpCheck += itemIsUnlocked[i];
        }
        if(addUpCheck == 0){
            levelClear = true;
        }

        if(levelClear){
            this.gameObject.SetActive(false);
            hiddenObj.SetActive(true);
        }

    }
}
