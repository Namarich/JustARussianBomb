using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log("nextlevel");
                collision.gameObject.GetComponent<Player>().key.SetActive(false);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BeatALevel();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().NewLevel();
            }

        }
    }
}
