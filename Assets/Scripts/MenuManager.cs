using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject theLevels;

    private GameObject clickedButton;

    public List<GameObject> buttons;

    public Color disabledColor;

    public GameObject tutorial;
    public GameObject tutorialButton;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        theLevels.SetActive(false);
        tutorial.SetActive(false);
        tutorialButton.SetActive(false);

        if (PlayerPrefs.GetInt("maxLevel") <= 1)
        {
            PlayerPrefs.SetInt("maxLevel", 1);
        }

        

        
    }

    // Update is called once per frame
    void Update()
    {
        clickedButton = EventSystem.current.currentSelectedGameObject;   
    }

    public bool CheckIfUnlocked(GameObject button)
    {
        Debug.Log(PlayerPrefs.GetInt("maxLevel"));
        if (System.Convert.ToInt32(button.transform.GetChild(0).GetComponent<TMP_Text>().text) <= PlayerPrefs.GetInt("maxLevel"))
        {
            Debug.Log("true");
            return true;
        }
        return false;
    }

    public void Change()
    {
        mainMenu.SetActive(!mainMenu.active);
        theLevels.SetActive(!theLevels.active);
        if (PlayerPrefs.GetInt("maxLevel") <= 1)
        {
            PlayerPrefs.SetInt("maxLevel", 1);
            tutorial.SetActive(true);
        }
        tutorialButton.SetActive(true);

        Debug.Log("change");

        for (int i = 1; i <= buttons.Count; i++)
        {
            if (PlayerPrefs.GetFloat(i.ToString() + "time") < 1)
            {
                PlayerPrefs.SetFloat(i.ToString() + "time", 10000f);
            }
            Debug.Log(PlayerPrefs.GetFloat(i.ToString() + "time"));
        }

        foreach (var button in buttons)
        {
            if (System.Convert.ToInt32(button.transform.GetChild(0).GetComponent<TMP_Text>().text) > PlayerPrefs.GetInt("maxLevel"))
            {
                button.GetComponent<Image>().color = disabledColor;
            }
            if (PlayerPrefs.GetFloat((buttons.IndexOf(button)+1).ToString() + "time") != 10000f)
            {
                button.transform.GetChild(1).GetComponent<TMP_Text>().text = PlayerPrefs.GetFloat((buttons.IndexOf(button)+1).ToString() + "time").ToString()+"s";
            }
        }
    }

    public void ShowTutorial()
    {
        tutorial.SetActive(!tutorial.active);
    }

    public void StartLevel()
    {
        Debug.Log(CheckIfUnlocked(clickedButton));
        if (CheckIfUnlocked(clickedButton))
        {
            TMP_Text b = clickedButton.transform.GetChild(0).GetComponent<TMP_Text>();
            int a = System.Convert.ToInt32(b.text);
            PlayerPrefs.SetInt("loadLevel", a);
            SceneManager.LoadScene("SampleScene");
        }
        
    }


}
