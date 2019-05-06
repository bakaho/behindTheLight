using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class pathFixed : MonoBehaviour {
    public PathCreator pathCreator;
    public EndOfPathInstruction EndInstruction;
    public float speed = 5;
    float distanceTraveled;

	// Use this for initialization
	void Start () {
        speed = Random.Range(20f, 50f);
	}
	
	// Update is called once per frame
	void Update () {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
        transform.rotation = Quaternion.identity;
		
	}

    public void resetDistanceOut(){
        speed = Random.Range(100f, 200f);
        distanceTraveled = 0;
    }

    public void resetDistanceIn()
    {
        speed = Random.Range(20f, 50f);
        distanceTraveled = 0;
    }
}
