using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int chosenCharacter;
    public GameObject[] characterPrefabs;

    public bool playerSpawned;

    public static bool isGameOver;
    public static bool isVictory;
    public static bool isGameActive;

    public GameObject GameOverPanel;
    public GameObject VictoryPanel;

    int level;

    public static bool isInBossRoom;
    public GameObject doorToBossRoom;

    public static bool isRestarted;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        isGameOver = false;
        isVictory = false;
        isInBossRoom = false;

        chosenCharacter = PlayerPrefs.GetInt("character");
        SpawnPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerLife.remainingMonster == 0) // || Input.GetKey(KeyCode.Space)
        {
            doorToBossRoom.gameObject.SetActive(false);
        }

        if (isInBossRoom == true)
        {
            doorToBossRoom.gameObject.SetActive(true);
        }

        if (BossMonster.BossIsDead)
        {
            Invoke("setVictoryPanel", 5.0f);
            isVictory = true;
            isGameActive = false;
        }

        if (PlayerLife.HP <= 0 && !BossMonster.BossIsDead)
        {
            Invoke("setGameOverPanel", 5.0f);
            isGameOver = true;
            isGameActive = false;
        }
    }

    void setVictoryPanel()
    {
        VictoryPanel.SetActive(true);
    }
    void setGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    void SpawnPlayer()
    {
        GameObject characterPrefab = characterPrefabs[chosenCharacter];
        Instantiate(characterPrefab, new Vector3(10, 2, -1), characterPrefab.transform.rotation);
        playerSpawned = true;
        isRestarted = false;
    }

}
