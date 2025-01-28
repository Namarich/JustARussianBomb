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

    public Color color;
    public Color defaultColor;

    private int cLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        otherTeleport.GetComponent<SpriteRenderer>().color = defaultColor;
        cLevel = PlayerPrefs.GetInt("level");
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
            Debug.Log("regular Level");
            
        }

        else if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().isRegularLevel)
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

        if (cLevel != PlayerPrefs.GetInt("level"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
            cLevel = PlayerPrefs.GetInt("level");
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
        gameObject.GetComponent<SpriteRenderer>().color = color;
        otherTeleport.GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(cooldown);
        gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        otherTeleport.GetComponent<SpriteRenderer>().color = defaultColor;
        collision.GetComponent<Player>().canTeleport = true;
    }
}
