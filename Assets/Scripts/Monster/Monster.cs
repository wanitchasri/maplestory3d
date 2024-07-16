using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int damage;

    public float speed;

    public float leftLimit;

    public float rightLimit;

    public int HP;

    public int HP_reduction;

    public Slider healthBar;

    public bool hasPotion;

    public GameObject[] potionPrefabs;

    public static bool getsHit;

    public static bool getsSkill1;

    public static bool getsSkill2;

    public static bool getsSkill3;

    private Transform target;

    private float targetPos;

    private string currentState = "IdleState";

    Animator monsterAnim;

    public bool hit;

    public bool attack;

    public bool idle;

    public AudioClip attackSound;

    private AudioSource monsterAudio;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        hasPotion = PotionPossibility();
        monsterAnim = GetComponent<Animator>();
        monsterAudio = GetComponent<AudioSource>();
        InvokeRepeating("MakeDamage", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = target.localPosition.x;
        healthBar.value = HP;

        LimitWalkingArea();

        if (targetPos < rightLimit && targetPos > leftLimit)
        {
            ChaseAndAttack();
        }
        else
        {
            WanderAround();
        }

        if (HP <= 0)
        {
            if (hasPotion)
            {
                SpawnPotion();
            }
            Destroy(this.gameObject);
            PlayerLife.remainingMonster -= 1;
        }
    }

    void LimitWalkingArea()
    {
        if (this.transform.localPosition.x > rightLimit)
        {
            this.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        if (this.transform.localPosition.x < leftLimit)
        {
            this.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    void WanderAround()
    {
        currentState = "IdleState";
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    bool PotionPossibility()
    {
        int rand = Random.Range(0, 10);
        if (rand > 5)
        {
            return true;
        }
        return false;
    }

    void SpawnPotion()
    {
        int potionIndex = Random.Range(0, potionPrefabs.Length);
        GameObject potionPrefab = potionPrefabs[potionIndex];
        Instantiate(potionPrefab,
        new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y + 5,
            gameObject.transform.position.z),
        potionPrefab.transform.rotation);
    }

    void MakeDamage()
    {
        if (currentState == "AttackState" && !PlayerController.isDodging)
        {
            PlayerLife.HP -= damage;
            PlayerController.isAttackedByMonster = true;
        }
    }

    IEnumerator getHit(float time)
    {
        idle = true;
        yield return new WaitForSecondsRealtime(time);
        idle = false;
    }

    void ChaseAndAttack()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        float chaseRange = rightLimit - leftLimit;
        float attackRange = 1;
        if (!idle)
        {
            switch (currentState)
            {
                case "IdleState":
                    if ((distance < chaseRange))
                    {
                        currentState = "ChaseState";
                    }
                    break;
                case "ChaseState":
                    monsterAnim.SetTrigger("Chase");
                    monsterAnim.SetBool("isAttacking", false);

                    if (distance < attackRange)
                    {
                        currentState = "AttackState";
                    }

                    //move towards the player
                    if (targetPos > transform.localPosition.x)
                    {
                        //move right
                        this.transform.localRotation =
                            Quaternion.Euler(0, 90, 0);
                        transform
                            .Translate(-transform.right *
                            speed *
                            Time.deltaTime);
                    }
                    else if (targetPos < transform.localPosition.x)
                    {
                        //move left
                        this.transform.localRotation =
                            Quaternion.Euler(0, -90, 0);
                        transform
                            .Translate(transform.right *
                            speed *
                            Time.deltaTime);
                    }
                    break;
                case "AttackState":
                    monsterAnim.SetBool("isAttacking", true);

                    if (distance > attackRange)
                    {
                        currentState = "ChaseState";
                    }

                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Attack" && getsHit)
        {
            StartCoroutine(getHit(0.5f));
            getsHit = false;
            healthBar.gameObject.SetActive(true);
            HP -= HP_reduction;
        }
        if (other.transform.tag == "Skill1" && getsSkill1)
        {
            StartCoroutine(getHit(0.5f));
            getsSkill1 = false;
            healthBar.gameObject.SetActive(true);
            HP -= HP_reduction * (int) 1.2;
        }
        if (other.transform.tag == "Skill2" && getsSkill2)
        {
            StartCoroutine(getHit(0.5f));
            getsSkill2 = false;
            healthBar.gameObject.SetActive(true);
            HP -= HP_reduction * (int) 1.5;
        }
        if (other.transform.tag == "Ultimate" && getsSkill3)
        {
            StartCoroutine(getHit(0.5f));
            getsSkill3 = false;
            healthBar.gameObject.SetActive(true);
            HP -= HP_reduction * (int) 1.8;
        }
    }
}
