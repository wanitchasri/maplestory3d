using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager_Test : MonoBehaviour
{
    public static int numberOfHP;
    public static int numberOfMP;
    public TextMeshProUGUI numberOfHPText;
    public TextMeshProUGUI numberOfMPText;

    public static int currentHealth = 50;
    public static int currentMana = 50;
    public Slider healthBar;
    public Slider manaBar;

    public static bool gameOver;

    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        numberOfHP = 0;
        numberOfMP = 0;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // currentHealth -= 1;
        //Display HP
        // numberOfHPText.text = "HP:" + numberOfHP;
        // Debug.Log("HP:" + numberOfHP);

        //Update the slider value
        healthBar.value = currentHealth;
        manaBar.value = currentMana;

        //gameOver
        if (currentHealth < 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            currentHealth = 50;
            currentMana = 50;
        }
    }
}
