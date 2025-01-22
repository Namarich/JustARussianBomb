using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    [System.Serializable]
    public class Level
    {
        public GameObject wholeLevel;
        public GameObject defaultLevel;
        public GameObject changedLevel;
        public Transform playerSpawnPoint;
        public int livesForThisLevel = 5;
    }

    public List<Level> levels;

    public int currentLevel = 0;

    public GameObject player;

    public GameObject shader;

    public ParticleSystem particles;

    public TMP_Text lifeText;
    public TMP_Text levelText;

    void Start()
    {
        NewLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlowUp(CircleCollider2D blowUpZone)
    {
        if (levels[currentLevel-1].livesForThisLevel >= 1)
        {
            levels[currentLevel - 1].livesForThisLevel -= 1;
            shader.SetActive(!shader.active);
            particles.Play();
            StartCoroutine(TriggerBlowUpZone(blowUpZone));
            levels[currentLevel - 1].defaultLevel.SetActive(!levels[currentLevel - 1].defaultLevel.active);
            levels[currentLevel - 1].changedLevel.SetActive(!levels[currentLevel - 1].changedLevel.active);
            lifeText.text = ":" + levels[currentLevel - 1].livesForThisLevel.ToString();
        }
        
    }

    public void NewLevel()
    {
        
        currentLevel += 1;
        if (currentLevel <= levels.Count)
        {
            for (int i = 0;i < levels.Count; i++)
            {
                levels[i].wholeLevel.SetActive(false);
            }
            shader.SetActive(false);
            levels[currentLevel - 1].wholeLevel.SetActive(true);
            lifeText.text = ":" + levels[currentLevel - 1].livesForThisLevel.ToString();
            player.transform.position = levels[currentLevel - 1].playerSpawnPoint.position;
            levels[currentLevel - 1].defaultLevel.SetActive(true);
            levels[currentLevel - 1].changedLevel.SetActive(false);
            levelText.text = "Level " + currentLevel.ToString();
        }
        else
        {
            Debug.Log("no more levelse");
        }
        
    }

    IEnumerator TriggerBlowUpZone(CircleCollider2D b)
    {
        b.enabled = true;
        yield return new WaitForSeconds(0.1f);
        b.enabled = false;
    }
}
