using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Player>().hasKey)
        {
            Follow();
        }
    }

    public void Follow()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 distance = player.transform.position - transform.position;

        if (distance.magnitude >= 1.3)
        {
            Vector3 targetPoint = player.transform.position - distance.normalized;

            gameObject.transform.position =
                Vector3.MoveTowards(gameObject.transform.position, targetPoint, 15 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.tag == "Player")
        {
            if (collision != GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().blowUpZone)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hasKey = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().key = gameObject;
            }
            
        }
    }
}
