using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class darkCurtainControl : MonoBehaviour {
    static public int nextGoodOrBad;
    public Animator earth;
    public GameObject borderJ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveToNext(){
        if(nextGoodOrBad == 0){
            //SceneManager.LoadScene(sceneName: "PureBad");
        }else{
            //SceneManager.LoadScene(sceneName: "PureGood");
        }
        borderJ.SetActive(true);
        earth.SetBool("isEarthquaking", false);
        this.gameObject.SetActive(false);
    }

    public void toGame(){
        SceneManager.LoadScene(sceneName: "oneForAll");
        //this.gameObject.SetActive(false);
    }

    public void closeMe(){
        this.gameObject.SetActive(false);
    }
}
