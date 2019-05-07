using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startMenuManager : MonoBehaviour {
    [Header("Percentage Bar")]
    public Slider progressBar;

	// Use this for initialization
	void Start () {
        progressBar.value = PlayerPrefs.GetFloat("percentage", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
