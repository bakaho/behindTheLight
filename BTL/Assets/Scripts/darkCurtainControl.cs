using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class darkCurtainControl : MonoBehaviour {
    static public int nextGoodOrBad;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveToNext(){
        if(nextGoodOrBad == 0){
            SceneManager.LoadScene(sceneName: "PureBad");
        }else{
            SceneManager.LoadScene(sceneName: "PureGood");
        }
        this.gameObject.SetActive(false);
    }
}
