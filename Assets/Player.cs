using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;

    public bool canTeleport = true;

    public bool hasKey = false;


    public void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(moveHorizontal, 0f);
        transform.Translate(direction * speed * Time.deltaTime);

        //rb.velocity += Vector2.right * moveHorizontal * speed;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BlowUp();
        }
    }

    public void Jump(float power)
    {
        rb.velocity += Vector2.up * power;
    }



}
