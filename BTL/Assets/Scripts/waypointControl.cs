using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class waypointControl : MonoBehaviour {
    public int myModule = 1;
    public bool fixX = false;
    public int xDir = 1;
    public bool fixY = false;
    public int yDir = 1;
    Waypoint retrieveWp = new Waypoint();
    public int tempTimes = 0; //save to local


	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey(GameManager.moduleTriggerTimes[myModule])){
            tempTimes = PlayerPrefs.GetInt(GameManager.moduleTriggerTimes[myModule]);
            print("[loacl storage] Retrieved trigger" +myModule +" temp times!!");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateToDatabase(){
        print("[Online Database] Retrieving");
        RestClient.Get<Waypoint>("https://behindthelight-f424f.firebaseio.com/" + myModule + ".json").Then(onResolved:response =>
        {
            retrieveWp = response;
            tempTimes = retrieveWp.times + 1;
            print(tempTimes);
            Waypoint wp = new Waypoint();
            wp.wpModule = myModule;
            wp.times = tempTimes;
            PlayerPrefs.SetInt(GameManager.moduleTriggerTimes[myModule], tempTimes);
            print("[Online Database] Storing");
            RestClient.Put("https://behindthelight-f424f.firebaseio.com/" + myModule + ".json",wp);
            //updateTempTimes();
        });
        print("[Online Database] Finished");

    }

}
