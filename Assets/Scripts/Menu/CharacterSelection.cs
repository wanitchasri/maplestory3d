using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    private Button button;

    private MenuManager menuManager;

    public GameObject character;
    public int character_id;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CharacterSelectStage);
        menuManager =
            GameObject.Find("Menu Manager").GetComponent<MenuManager>();
    }

    void CharacterSelectStage()
    {

        if (gameObject.transform.name == "MaleSelection")
        {
            character_id = 0;
        } 
        else if (gameObject.transform.name == "FemaleSelection")
        {
            character_id = 1;
        }
        PlayerPrefs.SetInt("character", character_id);

        menuManager.character = character;
        menuManager.StartGame();
    }
}
