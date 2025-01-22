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

    public bool startFromTheBeginning;

    void Start()
    {
        if (startFromTheBeginning)
        {
            PlayerPrefs.SetInt("level", 1);
        }
        else
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level"));
        }

        currentLevel = PlayerPrefs.GetInt("level");
        NewLevel();
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = PlayerPrefs.GetInt("level");
    }

    public void BlowUp(CircleCollider2D blowUpZone)
    {
        currentLevel = PlayerPrefs.GetInt("level");
        Debug.Log(PlayerPrefs.GetInt("level"));
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
        if (currentLevel <= levels.Count)
        {
            Debug.Log(currentLevel);
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

    public void BeatALevel()
    {
        if (currentLevel <= levels.Count)
        {
            currentLevel = PlayerPrefs.GetInt("level") + 1;
            Debug.Log(currentLevel);
            PlayerPrefs.SetInt("level", currentLevel);
        }
        
    }

    IEnumerator TriggerBlowUpZone(CircleCollider2D b)
    {
        b.enabled = true;
        yield return new WaitForSeconds(0.1f);
        b.enabled = false;
    }
}
