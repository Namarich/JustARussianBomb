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

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        theLevels.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        clickedButton = EventSystem.current.currentSelectedGameObject;

        if (clickedButton)
        {
            Debug.Log(clickedButton.transform.GetChild(0).GetComponent<TMP_Text>().text);
        }
        
    }

    public void Change()
    {
        mainMenu.SetActive(!mainMenu.active);
        theLevels.SetActive(!theLevels.active);
    }

    public void StartLevel()
    {
        TMP_Text b = clickedButton.transform.GetChild(0).GetComponent<TMP_Text>();
        int a = System.Convert.ToInt32(b.text);
        PlayerPrefs.SetInt("loadLevel", a);
        SceneManager.LoadScene("SampleScene");
    }


}
