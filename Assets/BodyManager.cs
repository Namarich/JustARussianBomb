using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{

    public GameObject main;

    public float waitAtTheStartOfTheScene;

    [System.Serializable]
    public class oneSprite
    {
        public Sprite sprite;
        public float duration;
    }

    public List<oneSprite> sprites;

    private int index = 0;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ChangeSprite()
    {
        main.GetComponent<SpriteRenderer>().sprite = sprites[index].sprite;
        yield return new WaitForSeconds(sprites[index].duration);
        if (index + 1 < sprites.Count)
        {
            index++;
            StartCoroutine(ChangeSprite());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitAtTheStartOfTheScene);

        StartCoroutine(ChangeSprite());
    }
}
