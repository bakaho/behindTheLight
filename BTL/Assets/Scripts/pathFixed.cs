﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class pathFixed : MonoBehaviour {
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTraveled;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
        transform.rotation = Quaternion.identity;
		
	}
}
