using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int cooldown;

    public Transform otherTeleport;

    public Sprite circleTeleport;
    public Sprite groundTeleport;

    public bool isGroundOnRegular = true;
    public bool isGroundOnChanged = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().isRegularLevel)
        {
            if (isGroundOnRegular)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = groundTeleport;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = circleTeleport;
            }
            
        }
        if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().isRegularLevel)
        {
            if (isGroundOnChanged)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = groundTeleport;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = circleTeleport;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().canTeleport)
            {
                
                collision.transform.position = otherTeleport.transform.position;
                
                collision.GetComponent<Player>().canTeleport = false;

                StartCoroutine(Cooldown(collision));
            }
            
        }
    }

    IEnumerator Cooldown(Collider2D collision)
    {
        yield return new WaitForSeconds(cooldown);
        collision.GetComponent<Player>().canTeleport = true;
    }
}
