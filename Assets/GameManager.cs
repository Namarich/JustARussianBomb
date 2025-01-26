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
        public int maxLives = 5;
        public GameObject key;
        public Transform keySpawnPoint;
        public Transform keyDefaultParent;
    }

    public List<Level> levels;

    public int currentLevel = 0;

    public GameObject player;

    public GameObject shader;

    public ParticleSystem particles;

    public TMP_Text lifeText;
    public TMP_Text levelText;

    public bool startFromTheBeginning;

    public GameObject pauseMenu;
    public Toggle toggle;

    private bool isShader = true;

    public bool isRegularLevel;

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

    public void ChangeShader()
    {
        isShader = toggle.isOn;
        Debug.Log(isShader);
        if (!isShader)
        {
            shader.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = PlayerPrefs.GetInt("level");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            NewLevel();
        }
    }

    public void BlowUp(CircleCollider2D blowUpZone)
    {
        currentLevel = PlayerPrefs.GetInt("level");
        Debug.Log(PlayerPrefs.GetInt("level"));
        pauseMenu.SetActive(false);
        if (levels[currentLevel-1].livesForThisLevel >= 1)
        {
            levels[currentLevel - 1].livesForThisLevel -= 1;
            if (isShader)
            {
                shader.SetActive(!shader.active);
            }
            particles.Play();
            StartCoroutine(TriggerBlowUpZone(blowUpZone));
            levels[currentLevel - 1].defaultLevel.SetActive(!levels[currentLevel - 1].defaultLevel.active);
            levels[currentLevel - 1].changedLevel.SetActive(!levels[currentLevel - 1].changedLevel.active);
            lifeText.text = ":" + levels[currentLevel - 1].livesForThisLevel.ToString();
            isRegularLevel = levels[currentLevel - 1].defaultLevel.active;
        }
        
    }

    public void ActivateMenu()
    {
        pauseMenu.SetActive(!pauseMenu.active);
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
            levels[currentLevel - 1].livesForThisLevel = levels[currentLevel - 1].maxLives;
            lifeText.text = ":" + levels[currentLevel - 1].livesForThisLevel.ToString();
            player.GetComponent<Player>().hasKey = false;
            player.GetComponent<Player>().key = null;
            particles.Stop();
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            levels[currentLevel - 1].key.transform.position = levels[currentLevel - 1].keySpawnPoint.position;
            levels[currentLevel - 1].key.transform.parent = levels[currentLevel - 1].keyDefaultParent;
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
