using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Sprite defaul;
    public Sprite killed;

    public bool hasBeenKilled = false;

    public ParticleSystem bloodParticles;

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
                if (collision != GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().blowUpZone && !hasBeenKilled)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().soundManager.PlaySound(4);
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().wasKilled = true;
                    collision.gameObject.GetComponent<Player>().key.SetActive(false);
                    StartCoroutine(Wait());
                }
                
            }

        }
    }

    public IEnumerator Wait()
    {
        hasBeenKilled = true;
        bloodParticles.Play();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().soundManager.PlaySound(4);
        yield return new WaitForSeconds(0.15f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ShowTime();
        gameObject.GetComponent<SpriteRenderer>().sprite = killed;
        gameObject.transform.position += new Vector3(0, 0.2f, 0f);
        yield return new WaitForSeconds(1.85f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BeatALevel(gameObject);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().NewLevel();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartOfTheLevel = Time.time;
    }
}
