﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myLight : MonoBehaviour {
    [Header("Initial Objects")]
    //GameManager
    public GameObject GM;
    //Game Objects
    public GameObject groundObj;
    static Animator groundAnim;
    public Transform CameraTransform;
    private Vector3 cameraOffset;
    //camera

    [Header("Initial Values")]
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.01f;
    public bool startPassed = false; //start trigger

    [Header("Movement Settings")]
    //move
    public ControlMove joystick;
    public float speed = 15;
    public static bool inControl = true;
    //cam

    //force apart
    static public float xApart = 0;
    static public float yApart = 0;
    static public float xApartOrg = 0;
    static public float yApartOrg = 0;
    public float hNet = 0;
    public float vNet = 0;


    //form
    //public static int lightShape = 0;
    //public static bool showT3 = false;
    //public static bool shapeChange = false;


    //UI system
    [Header("UI System")]
    public Text lockedRmd;
    public Image itemDropRmd;
    public Image theFirstBlock;
    public Sprite firstBlockImg;

    [Header("Special Effects")]
    //earthquake
    public bool earthquakeOn = false;
    public GameObject EQSound;
    public Image darkCurtain;

	// Use this for initialization
	void Start () {
        joystick = GameObject.FindWithTag("joystick").GetComponent<ControlMove>();
        groundAnim = groundObj.GetComponent<Animator>();

        //initialization of camera
        CameraTransform = GameObject.FindWithTag("MainCamera").transform;
        cameraOffset = CameraTransform.transform.position - new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

        //if it is on
        if (!GameManager.isPaused)
        {

            //character movement
            hNet = joystick.Horizontal(); //+FANXIANG
            vNet = joystick.Vertical(); //+FANXIANG
            float h = joystick.Horizontal() - xApart; //+FANXIANG
            float v = joystick.Vertical() - yApart; //+FANXIANG
            bool move = (Mathf.Abs(v - 0) > 0.001f) || (Mathf.Abs(h - 0) > 0.001f);
            if (move && CameraTilt.canMove && inControl)
            {
                //Vector3 newPos = transform.position + new Vector3(speed * h * Time.deltaTime, 0, speed * v * Time.deltaTime);
                transform.position += new Vector3(speed * h * Time.deltaTime, 0, speed * v * Time.deltaTime);

            }
            Vector3 newCamPos = transform.position + cameraOffset;

            //camera movement
            CameraTransform.position = Vector3.Slerp(CameraTransform.position, newCamPos, smoothFactor);

            //update acc
            if (xApartOrg > 0 && xApart > 0)
            {
                xApart -= 0.05f;
            }
            if (xApartOrg > 0 && xApart <= 0)
            {
                xApart = 0f;
            }
            if (xApartOrg < 0 && xApart < 0)
            {
                xApart += 0.05f;
            }
            if (xApartOrg < 0 && xApart >= 0)
            {
                xApart = 0f;
            }

            if (yApartOrg > 0 && yApart > 0)
            {
                yApart -= 0.05f;
            }
            if (yApartOrg > 0 && yApart <= 0)
            {
                yApart = 0f;
            }
            if (yApartOrg < 0 && yApart < 0)
            {
                yApart += 0.05f;
            }
            if (yApartOrg < 0 && yApart >= 0)
            {
                yApart = 0f;
            }
        }
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (!GameManager.isPaused)
        {
            if (other.gameObject.CompareTag("storyText"))
            {
                if (!other.GetComponent<storyTextControl>().isTriggered)
                {
                    other.GetComponent<storyTextControl>().isTriggered = true;
                    //other.GetComponentInChildren<lightUpText>().turnedOn = true;
                    other.gameObject.transform.GetChild(1).gameObject.GetComponent<lightUpText>().turnedOn = true;

                    //showNext
                    other.GetComponent<storyTextControl>().showNext();

                }
                other.GetComponent<storyTextControl>().changeTime();


            }

            if (other.gameObject.CompareTag("puzzleText"))
            {

                if (!other.GetComponent<puzzleTextControl>().isTriggered)
                {
                    other.GetComponent<puzzleTextControl>().isTriggered = true;

                    //show hint
                    other.GetComponent<puzzleTextControl>().showHint();
                    //other.gameObject.transform.GetChild(1).gameObject.GetComponent<lightUpText>().turnedOn = true;

                }
                other.GetComponent<puzzleTextControl>().changeTime();


            }



            if (other.gameObject.CompareTag("locked"))
            {
                xApart = 2 * hNet;
                yApart = 2 * vNet;
                xApartOrg = 2 * hNet;
                yApartOrg = 2 * vNet;
                lockedRmd.gameObject.SetActive(true);
                //Handheld.Vibrate();
                GM.GetComponent<GameManager>().playLockSound();

                other.GetComponent<lockedAreaControl>().checkUnlock();
            }

            if (other.gameObject.CompareTag("outerline"))
            {
                xApart = 2 * hNet;
                yApart = 2 * vNet;
                xApartOrg = 2 * hNet;
                yApartOrg = 2 * vNet;
                //Handheld.Vibrate();
                GM.GetComponent<GameManager>().playLockSound();
            }

            //earthquakeEffect
            if (other.gameObject.CompareTag("earthquake") && !earthquakeOn)
            {
                earthquakeOn = true;
                groundAnim.SetBool("isEarthquaking", true);
                EQSound.gameObject.SetActive(true);
            }

            if (other.gameObject.CompareTag("badline") && earthquakeOn)
            {
                print("bad line");
                darkCurtain.gameObject.SetActive(true);
                darkCurtainControl.nextGoodOrBad = 0;
            }

            if (other.gameObject.CompareTag("goodline") && earthquakeOn)
            {
                print("good line");
                darkCurtain.gameObject.SetActive(true);
                darkCurtainControl.nextGoodOrBad = 1;
            }

            if (other.gameObject.CompareTag("start") && !startPassed)
            {
                if (GM.GetComponent<GameManager>().gameLoop == 0)
                {
                    itemDropRmd.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = firstBlockImg;
                    itemDropRmd.gameObject.SetActive(true);
                    theFirstBlock.GetComponent<slotControl>().changeItemImg(firstBlockImg, "记忆通行符：\n这是记忆大陆唯一的通行证。\n请带上它上路，收集另外<b><color=red>"+GameManager.collectTotalNum+"个</color></b>记忆碎片，走向无限光明的终点。离开时，系统会将它和记忆碎片一并回收。祝你好运。", 0);
                    theFirstBlock.GetComponent<slotControl>().turnOn();
                }
                startPassed = true;
            }
        }
    }
}
