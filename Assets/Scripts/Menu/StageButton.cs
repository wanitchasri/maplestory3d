using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    private Button button;

    private MenuManager menuManager;

    public int stage;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener (SelectStage);
        menuManager =
            GameObject.Find("Menu Manager").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SelectStage()
    {
        //Debug.Log(gameObject.name + " was clicked");
        menuManager.stage = stage;
        menuManager.SelectCharacter();
    }
}
