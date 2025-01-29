using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;

    public bool canTeleport = true;

    public bool hasKey = false;

    public GameObject key;

    public float blowUpRadius = 10f;

    public CircleCollider2D blowUpZone;

    private float maxJumpPower;

    public List<GameObject> DisabledWalls;

    public void Start()
    {
        blowUpZone.radius = blowUpRadius;
    }

    public void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(moveHorizontal, 0f);
        transform.Translate(direction * speed * Time.deltaTime);

        //rb.velocity += Vector2.right * moveHorizontal * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BlowUp(blowUpZone);
        }

        if (rb.velocity.y > maxJumpPower)
        {
            rb.velocity = Vector2.up * maxJumpPower;
        }

        
    }

    public void Jump(float power)
    {
        rb.velocity = Vector2.up * power;
        maxJumpPower = power;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BlockingWall")
        {
            collision.transform.gameObject.SetActive(false);
            DisabledWalls.Add(collision.gameObject);
        }
    }



}
