using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public static int HP;
    public static int MP;

    public int totalMonster;
    public static int remainingMonster;
    public TextMeshProUGUI remainingMonsterText;

    //public static int currentHealth = 50;
    //public static int currentMana = 50;
    public Slider healthBar;
    public Slider manaBar;

    // public static bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        remainingMonster = totalMonster;
        HP = 100;
        MP = 100;
        // gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP >= 100)
        {
            HP = 100;
        }
        if (MP >= 100)
        {
            MP = 100;
        }

        // currentHealth -= 1;
        //Display HP
        remainingMonsterText.text = "Alive: " + remainingMonster;
        // Debug.Log("HP:" + numberOfHP);

        //Update the slider value
        healthBar.value = HP;
        manaBar.value = MP;

        //gameOver
        //if (currentHealth < 0)
        //{
        //    gameOver = true;
        //}
    }

}
