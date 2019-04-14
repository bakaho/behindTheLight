using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour {
    public static bool canMove = true;

    public float camSpeed = 1f;
    Vector3 accSmooth;
    public float AccelerometerUpdateInterval = 0.01f;
    public float LowPassKernelWidthInSeconds = 0.001f;
    public Vector3 lowPassValue = Vector3.zero;
    public Vector3 initAngle;


    // Use this for initialization
    void Start()
    {
        initAngle = lowPassValue;
    }

    // Update is called once per frame
    void Update()
    {

        accSmooth = lowpass();
        float xRot = accSmooth.x;
        float yRot = accSmooth.y;

        float xGap = xRot - initAngle.x;
        float yGap = yRot - initAngle.y;

        float turnX = 0;
        float turnY = 0;

        if (yGap > 0.5)
        {
            turnY = 5;
        }
        else if (yGap < -0.5)
        {
            turnY = -5;
        }
        else
        {
            turnY = yGap * 10;
        }

        if (xGap > 0.3)
        {
            turnX = 6;
        }
        else if (xGap < -0.3)
        {
            turnX = -6;
        }
        else
        {
            turnX = xGap * 20;
        }

        //if(xRot < -1f){
        //    xRot = -1f;
        //}else if(xRot > 1f){
        //    xRot = 1f;
        //}
        //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(-yRot, -xRot, 0, camSpeed), Time.deltaTime * camSpeed);
        if (canMove)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(30 + turnY, -turnX, 0), Time.deltaTime * camSpeed);
        }

    }

    Vector3 lowpass()
    {
        float lowPassFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
        lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, lowPassFactor);
        return lowPassValue;
    }
}
