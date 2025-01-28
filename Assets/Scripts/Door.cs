using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Sprite defaul;
    public Sprite killed;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = defaul;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().hasKey)
            {
                if (collision != GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().blowUpZone)
                {
                    Debug.Log("nextlevel");
                    collision.gameObject.GetComponent<Player>().key.SetActive(false);
                    gameObject.GetComponent<SpriteRenderer>().sprite = killed;
                    gameObject.transform.position += new Vector3(0,0.2f,0f);
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().wasKilled = true;
                    StartCoroutine(Wait());
                }
                
            }

        }
    }

    public IEnumerator Wait()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ShowTime();
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BeatALevel();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().NewLevel();
    }
}
