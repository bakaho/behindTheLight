using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myLight : MonoBehaviour {
    //move
    public ControlMove joystick;
    public float speed = 15;
    public static bool inControl = true;
    //cam
    public Transform CameraTransform;
    private Vector3 cameraOffset;
    //hit
    //public static int theTriggerOne = -1;
    //form
    public static int lightShape = 0;
    public static bool shapeChange = false;

    //testing
    public static bool showT3 = false;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.01f;

	// Use this for initialization
	void Start () {
        joystick = GameObject.FindWithTag("joystick").GetComponent<ControlMove>();

        //initialization of camera
        CameraTransform = GameObject.FindWithTag("MainCamera").transform;
        cameraOffset = CameraTransform.transform.position - new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        //character movement
        float h = joystick.Horizontal();
        float v = joystick.Vertical();
        bool move = (Mathf.Abs(v - 0) > 0.001f) || (Mathf.Abs(h - 0) > 0.001f);
        if (move && CameraTilt.canMove && inControl)
        {
            //Vector3 newPos = transform.position + new Vector3(speed * h * Time.deltaTime, 0, speed * v * Time.deltaTime);
            transform.position += new Vector3(speed * h * Time.deltaTime, 0, speed * v * Time.deltaTime);

        }
        Vector3 newCamPos = transform.position + cameraOffset;

        //camera movement
        CameraTransform.position = Vector3.Slerp(CameraTransform.position, newCamPos, smoothFactor);
		
	}

    void OnTriggerEnter(Collider other)
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
        }
    }
}
