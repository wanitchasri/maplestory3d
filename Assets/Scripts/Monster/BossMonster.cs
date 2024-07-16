using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{
    public int damage;

    public float speed;

    public float leftLimit;

    public float rightLimit;

    public int HP;

    public int HP_reduction;

    public Slider healthBar;

    private Transform target;

    private float targetPos;

    private string currentState = "IdleState";

    public static bool getsHit;

    public static bool getsSkill1;

    public static bool getsSkill2;

    public static bool getsSkill3;

    public GameObject projectilePrefab1;

    public GameObject projectilePrefab2;

    Animator bossAnim;

    private AudioSource bossAudio;
    public AudioClip attackSound;
    public AudioClip bossRoomSound;
    public AudioClip victoryMusic;
    public AudioClip gameOverMusic;

    public static bool BossIsDead = false;

    public bool music = false;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        bossAnim = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
        InvokeRepeating("AttackPlayer", 2f, 2f);

        BossIsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameActive)
        {

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetPos = target.localPosition.x;
            healthBar.value = HP;

            if (target.transform.position.x > leftLimit)
            {
                GameManager.isInBossRoom = true;
                // ChaseAndAttack();
            }

            if (HP <= 0)
            {
                BossIsDead = true;
                bossAudio.Stop();
            }

            if (PlayerLife.HP <= 0)
            {
                bossAudio.Stop();
            }
        }
        else if (!GameManager.isGameActive)
        {
            if (GameManager.isVictory)
            {
                bossAudio.PlayOneShot(victoryMusic, 2.0f);
            }
            else if (GameManager.isGameOver)
            {
                bossAudio.PlayOneShot(gameOverMusic, 2.0f);
            }
        }

    }

    void AttackPlayer()
    {
        if (GameManager.isInBossRoom && !BossIsDead)
        {
            bossAudio.PlayOneShot(attackSound, 2.0f);

            if (!music)
            {
                music = true;
                bossAudio.PlayOneShot(bossRoomSound, 2.0f);
            }

            //transform.LookAt(target.position);
            Instantiate(projectilePrefab1,
            projectilePrefab1.transform.position,
            projectilePrefab1.transform.rotation);

            //Instantiate(projectilePrefab2,
            //transform.position,
            //transform.rotation);

        }
    }

    void ChaseAndAttack()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        float chaseRange = rightLimit - leftLimit;
        float attackRange = 1;

        switch (currentState)
        {
            case "IdleState":
                if (distance < chaseRange)
                {
                    currentState = "ChaseState";
                }
                break;
            case "ChaseState":
                //bossAnim.SetTrigger("Chase");
                //bossAnim.SetBool("isAttacking", false);
                if (distance < attackRange)
                {
                    currentState = "AttackState";
                }

                //move towards the player
                if (targetPos > transform.localPosition.x)
                {
                    //move right
                    this.transform.localRotation = Quaternion.Euler(0, 90, 0);
                    transform
                        .Translate(-transform.right * speed * Time.deltaTime);
                }
                else if (targetPos < transform.localPosition.x)
                {
                    //move left
                    this.transform.localRotation = Quaternion.Euler(0, -90, 0);
                    transform
                        .Translate(transform.right * speed * Time.deltaTime);
                }
                break;
            case "AttackState":
                //bossAnim.SetBool("isAttacking", true);
                if (distance > attackRange)
                {
                    currentState = "ChaseState";
                }

                break;
        }
    }

    void MakeDamage()
    {
        if (currentState == "AttackState")
        {
            Instantiate(projectilePrefab1,
            transform.position,
            projectilePrefab1.transform.rotation);
            Instantiate(projectilePrefab2,
            transform.position,
            projectilePrefab2.transform.rotation);
            PlayerLife.HP -= damage;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (getsHit)
            {
                getsHit = false;
                healthBar.gameObject.SetActive(true);
                HP -= HP_reduction;
                getsHit = false;
            }

            if (getsSkill1)
            {
                getsSkill1 = false;
                healthBar.gameObject.SetActive(true);
                HP -= HP_reduction * (int) 1.2;
                getsSkill1 = false;
            }

            if (getsSkill2)
            {
                getsSkill2 = false;
                healthBar.gameObject.SetActive(true);
                HP -= HP_reduction * (int) 1.5;
                getsSkill2 = false;
            }

            if (getsSkill3)
            {
                getsSkill3 = false;
                healthBar.gameObject.SetActive(true);
                HP -= HP_reduction * (int) 1.8;
                getsSkill3 = false;
            }
        }
    }
}
