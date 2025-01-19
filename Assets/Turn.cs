using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{

    public GameObject knife;

    public GameObject rotationPoint;

    public float turnSpeed = 60f;

    public GameObject leftHandle;

    public GameObject rotationPointLeftHandle;

    public GameObject rightHandle;

    public GameObject rotationPointRightHandle;

    public GameObject knifeRay;

    public LayerMask lmask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                knife.transform.RotateAround(rotationPoint.transform.position, Vector3.forward, turnSpeed * Time.deltaTime);
            }
            else
            {
                knife.transform.RotateAround(rotationPoint.transform.position, Vector3.back, turnSpeed * Time.deltaTime);
            }

            RaycastHit2D a = Physics2D.Raycast(knifeRay.transform.position, Vector2.down,Mathf.Infinity,lmask);
            if (a)
            {
                Debug.DrawLine(knifeRay.transform.position,a.transform.position);
                Debug.Log("it hit THE GROUND");
            }


        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                leftHandle.transform.RotateAround(rotationPointLeftHandle.transform.position, Vector3.forward, turnSpeed * Time.deltaTime);
            }
            else
            {
                leftHandle.transform.RotateAround(rotationPointLeftHandle.transform.position, Vector3.back, turnSpeed * Time.deltaTime);
            }

        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rightHandle.transform.RotateAround(rotationPointRightHandle.transform.position, Vector3.back, turnSpeed * Time.deltaTime);
            }
            else
            {
                rightHandle.transform.RotateAround(rotationPointRightHandle.transform.position, Vector3.forward, turnSpeed * Time.deltaTime);
            }

        }
    }
}
