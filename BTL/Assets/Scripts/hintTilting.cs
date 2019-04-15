using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintTilting : MonoBehaviour {

    public float rotSpeed = 5;
    public Camera uiCam;
    bool rotateAllow = false;
    Touch initTouch;
    Vector3 initMouse;

    // Update is called once per frame
    private void Start()
    {
        uiCam = GameObject.FindWithTag("UICam").GetComponent<Camera>();
    }
    void Update()
    {
        //rotate to play around with the floating 3d ui

        //if start drag from inside of the object
        if (Input.GetMouseButtonDown(0))
        {
            myLight.inControl = false;
            initMouse = Input.mousePosition;
            Ray ray = uiCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    print("hit!");
                    rotateAllow = true;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {

            float xMoved = Input.mousePosition.x - initMouse.x;
            float yMoved = Input.mousePosition.y - initMouse.y;
            float distance = Mathf.Sqrt((xMoved * xMoved) + (xMoved * xMoved));
            if (rotateAllow)
            {
                float rotX = xMoved / 5f * rotSpeed * Mathf.Deg2Rad;
                float rotY = yMoved / 5f * rotSpeed * Mathf.Deg2Rad;

                transform.Rotate(Vector3.up, -rotX);
                transform.Rotate(Vector3.right, rotY);
                myLight.inControl = false;
            }
            else
            {
                myLight.inControl = true;
            }

        }


        //if let go, start over again
        else if (Input.GetMouseButtonUp(0))
        {
            myLight.inControl = true;
            rotateAllow = false;
        }

        //print(myLight.inControl);



    }

}
