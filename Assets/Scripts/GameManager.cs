using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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

    [System.Serializable]
    public class DefaultShaderValues
    {
        public int pixelDensity;
        public float colorBrightness;
        public float waveAmplitude;
    }


    public List<Level> levels;

    public int currentLevel = 0;

    public GameObject player;

    public GameObject shader;

    public ParticleSystem particles;

    public TMP_Text lifeText;
    public TMP_Text levelText;

    public Material shaderObject;

    public bool startFromTheBeginning;

    public GameObject pauseMenu;
    public Toggle toggle;

    private bool isShader = true;

    public bool isRegularLevel;

    public Slider sliderPixel;
    public Slider sliderColor;
    public Slider sliderAmplitude;
    public Slider sliderMasterVolume;

    public DefaultShaderValues values;

    public bool wasKilled = false;

    public GameObject loseScreen;

    public float StartOfTheLevel;
    public TMP_Text speedrun;
    private double speedrunTime;

    public GameObject TimeTable;

    public SoundManager soundManager;

    public bool showPlayerCutscene;
    public GameObject cutsceneBase;

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

        if (PlayerPrefs.GetFloat("masterVolume") != 0)
        {
            sliderMasterVolume.value = PlayerPrefs.GetFloat("masterVolume");
        }

        if ((currentLevel == 1 && PlayerPrefs.GetInt("maxLevel") == 1) || showPlayerCutscene)
        {
            cutsceneBase.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            cutsceneBase.GetComponent<BoxCollider2D>().enabled = false;
        }



        DefaultShaderSettings();
        currentLevel = PlayerPrefs.GetInt("level");
        if(PlayerPrefs.GetInt("loadLevel") != 0)
        {
            PlayLevel(PlayerPrefs.GetInt("loadLevel"));
            PlayerPrefs.SetInt("loadLevel", 0);
        }
        else
        {
            NewLevel();
        }

        

        StartOfTheLevel = 0f;
        TimeTable.SetActive(false);
    }

    public void DefaultShaderSettings()
    {
        shaderObject.SetFloat("_PixelDensity", values.pixelDensity);
        shaderObject.SetFloat("_ColorBrightness", values.colorBrightness);
        shaderObject.SetFloat("_WaveAmplitude", values.waveAmplitude);
        sliderPixel.value = values.pixelDensity;
        sliderColor.value = values.colorBrightness;
        sliderAmplitude.value = values.waveAmplitude;

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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

        if (Input.GetKeyDown(KeyCode.Q) && !wasKilled)
        {
            NewLevel();
            StartOfTheLevel = Time.time;
        }

        shaderObject.SetFloat("_PixelDensity", sliderPixel.value);
        shaderObject.SetFloat("_ColorBrightness", sliderColor.value);
        shaderObject.SetFloat("_WaveAmplitude", sliderAmplitude.value);

        soundManager.GetComponent<SoundManager>().soundVolume = sliderMasterVolume.value;
        PlayerPrefs.SetFloat("masterVolume", sliderMasterVolume.value);
        
    }


    public void ShowTime()
    {
        TimeTable.SetActive(true);
        speedrunTime = ((Mathf.Round((Time.time - StartOfTheLevel) * 100)) / 100.0);
        speedrun.text = speedrunTime.ToString() + "s";
        if(speedrunTime < PlayerPrefs.GetFloat(currentLevel.ToString() + "time"))
        {
            PlayerPrefs.SetFloat(currentLevel.ToString() + "time", (float)speedrunTime);
        }
    }

    public void BlowUp(CircleCollider2D blowUpZone)
    {
        currentLevel = PlayerPrefs.GetInt("level");
        Debug.Log(PlayerPrefs.GetInt("level"));
        pauseMenu.SetActive(false);
        if (levels[currentLevel-1].livesForThisLevel >= 1 && !wasKilled)
        {
            soundManager.PlaySound(6);
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
        else if (!wasKilled)
        {
            soundManager.PlaySound(3);
            StartCoroutine(Lose());
            player.GetComponent<Player>().canTeleport = true;
        }
        
    }

    public IEnumerator Lose()
    {
        soundManager.PlaySound(3);
        loseScreen.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        loseScreen.SetActive(false);
        NewLevel();
        StartOfTheLevel = Time.time;
    }

    public void ActivateMenu()
    {
        pauseMenu.SetActive(!pauseMenu.active);
        if (pauseMenu.active)
        {
            TimeTable.SetActive(false);
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
            TimeTable.SetActive(false);
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
            isRegularLevel = true;
            for (int i = 0;i < player.GetComponent<Player>().DisabledWalls.Count; i++)
            {
                player.GetComponent<Player>().DisabledWalls[i].SetActive(true);
            }
            wasKilled = false;
            player.GetComponent<Player>().canTeleport = true;
            StartOfTheLevel = Time.time;
        }
        else
        {
            Debug.Log("no more levelse");
            StartOfTheLevel = Time.time;
        }
        
    }


    public void PlayLevel(int level)
    {
        if (level <= levels.Count)
        {
            currentLevel = level;
            PlayerPrefs.SetInt("level", currentLevel);
            NewLevel();
        }
    }

    public void BeatALevel(GameObject door)
    {
        if (currentLevel <= levels.Count && currentLevel >= PlayerPrefs.GetInt("level"))
        {
            currentLevel = PlayerPrefs.GetInt("level") + 1;
            Debug.Log(currentLevel);
            PlayerPrefs.SetInt("level", currentLevel);
            if (currentLevel > PlayerPrefs.GetInt("maxLevel"))
            {
                PlayerPrefs.SetInt("maxLevel", currentLevel);
            }
            
        }
        door.GetComponent<Door>().hasBeenKilled = false;

    }

    IEnumerator TriggerBlowUpZone(CircleCollider2D b)
    {
        b.enabled = true;
        yield return new WaitForSeconds(0.1f);
        b.enabled = false;
    }
}
