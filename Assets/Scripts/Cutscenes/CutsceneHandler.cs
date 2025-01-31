using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{

    public Camera cam;
    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;

    public string nextSceneName;

    public bool disablePlayer;

    public void Start()
    {
        cutsceneElements = GetComponents<CutsceneElementBase>();
        
    }

    private void ExecuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Length)
        {
            cutsceneElements[index].Execute();
        }
        else if (index >= cutsceneElements.Length && nextSceneName != "")
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            if (disablePlayer)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = true;
            }
        }
    }

    public void PlayNextElement()
    {
        if (disablePlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        }
        index++;
        ExecuteCurrentElement();
    }
}
