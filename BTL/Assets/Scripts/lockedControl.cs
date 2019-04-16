using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedControl : MonoBehaviour {
    //float xPos;
    //float yPos;
    //float plyX;
    //float plyY;
    //float radius;
    //public GameObject player;

	//// Use this for initialization
	//void Start () {
 //       xPos = this.transform.position.x;
 //       yPos = this.transform.position.y;

	//}
	
	//// Update is called once per frame
	//void Update () {
 //       if(Vector3.Distance(this.transform.position, player.transform.position)<radius){

 //       }
		
	//}
    public void hideRmd(){
        this.gameObject.SetActive(false);
    }
}
