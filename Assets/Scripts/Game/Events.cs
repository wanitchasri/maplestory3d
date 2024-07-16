using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public GameObject backToMenuScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            backToMenuScreen.SetActive(true);
        }
    }

    //replay level
    public void ReplayLevel()
    {
        transform.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        GameManager.isRestarted = true;
        //SceneManager.LoadScene("Stage1");
    }

    //quit game
    public void QuitGame()
    {
        SceneManager.LoadSceneAsync("MenuScene");
        GameManager.isRestarted = true;
    }

    public void cancelExit()
    {
        backToMenuScreen.SetActive(false);
    }

}
