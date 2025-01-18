using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{

    public GameObject knife;

    public GameObject rotationPoint;

    public float turnSpeed = 60f;
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
            
        }
    }
}
