using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    [System.Serializable]
    public class Level
    {
        public GameObject defaultLevel;
        public GameObject changedLevel;
        public Transform playerSpawnPoint;
        public int livesForThisLevel = 5;
    }

    public List<Level> levels;

    public int currentLevel = 1;

    public GameObject player;

    void Start()
    {
        levels[currentLevel - 1].defaultLevel.SetActive(true);
        player.transform.position = levels[currentLevel - 1].playerSpawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlowUp()
    {
        if (levels[currentLevel-1].livesForThisLevel >= 1)
        {
            levels[currentLevel - 1].livesForThisLevel -= 1;
            levels[currentLevel - 1].defaultLevel.SetActive(!levels[currentLevel - 1].defaultLevel.active);
            levels[currentLevel - 1].changedLevel.SetActive(!levels[currentLevel - 1].changedLevel.active);
        }
        
    }
}
