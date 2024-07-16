using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickToStart : MonoBehaviour
{
    public TextMeshProUGUI clickText;

    public GameObject stageButton;

    public GameObject exitScreen;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            exitScreen.SetActive(true);
        } 
        else if (Input.anyKeyDown && !(Input.GetKey(KeyCode.Escape)))
        {
            clickText.gameObject.SetActive(false);
            stageButton.gameObject.SetActive(true);
        }
    }

    public void cancelExit()
    {
        exitScreen.SetActive(false);
    } 

    public void exitGame()
    {
        Application.Quit();
    }
}
