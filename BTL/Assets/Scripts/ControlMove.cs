using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMove : MonoBehaviour {

    public float xDel, yDel;
    public Camera myCam;
    bool moveAllow = false;
    bool moveChecked = false;
    float touchStartTime = 0f;
    Touch initTouch;
    Vector3 initMouse;

    //show back shape
    public GameObject playerLight;
    public Texture circleShape;


    // Update is called once per frame
    private void Start()
    {
        myCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            initMouse = Input.mousePosition;
            moveAllow = true;
            touchStartTime = Time.time;

            //Ray ray = uiCam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit = new RaycastHit();
            //if (Physics.Raycast(ray, out hit))
            //{
            //    if (hit.transform.gameObject == this.gameObject)
            //    {
            //        print("hit!");
            //        rotateAllow = true;
            //    }
            //}
        }
        else if (Input.GetMouseButton(0))
        {
            float xMoved = Input.mousePosition.x - initMouse.x;
            float yMoved = Input.mousePosition.y - initMouse.y;
            //float distance = Mathf.Sqrt((xMoved * xMoved) + (xMoved * xMoved));
            moveChecked = (moveAllow && Mathf.Abs(xMoved) > 3 && Mathf.Abs(yMoved) > 3);

            if (moveChecked)
            {

                if (xMoved > 50)
                {
                    xDel = 1;
                }
                else if (xMoved < -50)
                {
                    xDel = -1;
                }
                else
                {
                    xDel = xMoved / 50f;
                }

                if (yMoved > 50)
                {
                    yDel = 1;
                }
                else if (yMoved < -50)
                {
                    yDel = -1;
                }
                else
                {
                    yDel = yMoved / 50f;
                }

            }
            if (Mathf.Abs(xMoved) < 3 && Mathf.Abs(yMoved) < 3)
            {
                float deltatime = Time.time - touchStartTime;
                if (deltatime > 2)
                {
                    //myLight.shapeChange = true;
                    //myLight.lightShape = 0;
                    playerLight.GetComponent<Light>().cookie = circleShape;
                    GameManager.curShapeM = 0;
                    GameManager.curShapeS = 0;
                    print(GameManager.curShapeM + " + " + GameManager.curShapeS);
                    CameraTilt.canMove = true;
                    myLight.inControl = true;
                }
            }

        }
        //if let go, start over again
        else if (Input.GetMouseButtonUp(0))
        {
            moveAllow = false;
            moveChecked = false;
        }
        //print("this: " + moveChecked);
        //print("x: " + xDel);
        //print("y: " + yDel);


    }

    public float Horizontal()
    {
        if (moveChecked)
        {
            //print("0");
            return xDel;

        }
        else
        {
            //print("1");
            return Input.GetAxis("Horizontal");

        }
    }

    public float Vertical()
    {
        if (moveChecked)
        {
            //print("2");
            return yDel;

        }
        else
        {
            //print("3");
            return Input.GetAxis("Vertical");

        }
    }

}
