using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public bool isGameActive = false;

    public GameObject titleScreen;
    public GameObject selectScreen;
    public int stage;

    public GameObject character;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCharacter()
    {
        titleScreen.gameObject.SetActive(false);
        selectScreen.gameObject.SetActive(true);

        mainCamera.transform.position = new Vector3(31.14f, 4.61f, -1f);
    }

    public void StartGame()
    {
        isGameActive = true;
        SceneManager.LoadScene("Stage1");
    }

}
