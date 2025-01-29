using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float power;

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
        if (collision.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>().blowUpZone != collision)
            {
                collision.transform.gameObject.GetComponent<Player>().Jump(power);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().soundManager.PlaySound(2);
            }
            
        }

        
    }
}
