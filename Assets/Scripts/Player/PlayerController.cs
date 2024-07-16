using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public float jumpForce;

    Rigidbody playerRb;

    Animator playerAnim;

    public float gravityModifier = 5.0f;

    public static bool isAttacking;

    public static bool isAttackedByMonster;
    public static bool isAttackedByBoss;

    public bool isOnGround = true;

    public bool isOnGround2 = true;

    public static bool isDodging = false;

    private AudioSource playerAudio;

    public AudioClip jumpSound;

    public AudioClip hitSound;

    public AudioClip attackSound;

    public AudioClip attackSound2;

    public AudioClip dodgeSound;

    public AudioClip getHitSound;
    public AudioClip explosionSound;

    public AudioClip skill2Sound;

    public AudioClip spawnSound;

    public AudioClip potionSound;

    public AudioClip victorySound;
    public AudioClip defeatSound;

    public ParticleSystem spawnParticle;
    public ParticleSystem hitParticle;
    public ParticleSystem victoryParticle;
    public ParticleSystem deadParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        playerAudio.PlayOneShot (spawnSound);
        spawnParticle.Play();

        playerAnim.SetBool("isWon", false);
        playerAnim.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isRestarted)
        {
            Debug.Log("Game Restarted");
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }

        if (GameManager.isGameActive)
        {
            UpdateMove();
            UpdatePotionUse();
            UpdateAbility();

            if (isAttackedByMonster)
            {
                playerAnim.SetBool("isHit", true);
                playerAudio.PlayOneShot(getHitSound);
                isAttackedByMonster = false;
            }
            else if (!isAttackedByMonster || !isAttackedByBoss)
            {
                playerAnim.SetBool("isHit", false);
            }

            if (isAttackedByBoss)
            {
                playerAnim.SetBool("isHit", true);
                playerAudio.PlayOneShot(explosionSound, 2.0f);
                hitParticle.Play();
                isAttackedByBoss = false;
            }

            else if (!isAttackedByBoss)
            {
                hitParticle.Stop();
            }
        } 
        else if (!GameManager.isGameActive)
        {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetBool("isAttacking", false);
            playerAnim.SetBool("isJumping", false);
            playerAnim.SetBool("isSpinning", false);


            if (GameManager.isVictory)
            {
                playerAudio.PlayOneShot(victorySound, 2.0f);
                playerAnim.SetBool("isWon", true);
                victoryParticle.Play();
            }
            else if (GameManager.isGameOver)
            {
                playerAudio.PlayOneShot(defeatSound, 2.0f);
                playerAnim.SetBool("isDead", true);
                deadParticle.Play(deadParticle);
            }
        }
    }

    void UpdateMove()
    {
        bool isAttacking = isAttacking = playerAnim.GetBool("isAttacking");
        bool isSpinning = playerAnim.GetBool("isSpinning");
        bool isWalking = playerAnim.GetBool("isWalking");
        bool isJumping = playerAnim.GetBool("isJumping");

        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool crouchPressed = Input.GetKey(KeyCode.S);
        bool jumpPressed = Input.GetKeyDown(KeyCode.K);

        //bool jumpPressed2 = Input.GetKeyDown(KeyCode.W);
        bool attackPressed = Input.GetKeyDown(KeyCode.J);
        bool dodgePressed = Input.GetKeyDown(KeyCode.L);
        bool skill1Pressed = Input.GetKeyDown(KeyCode.U);
        bool skill2Pressed = Input.GetKeyDown(KeyCode.I);

        // Attack
        if (
            attackPressed &&
            !isAttacking &&
            !Skill1.isSkill1 &&
            !Skill2.isSkill2 &&
            !BossMonster.BossIsDead
        )
        {
            StartCoroutine(AttackTime(0.5f));
        }
        if (
            !attackPressed &&
            !isAttacking &&
            jumpPressed &&
            isOnGround &&
            !isDodging &&
            !Skill1.isSkill1 &&
            !Skill2.isSkill2 &&
            !BossMonster.BossIsDead
        )
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            isOnGround = false;
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetBool("isJumping", true);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (
            dodgePressed &&
            !isDodging &&
            isOnGround2 &&
            !Skill1.isSkill1 &&
            !Skill2.isSkill2 &&
            !BossMonster.BossIsDead
        )
        {
            StartCoroutine(DodgeTime(0.25f));
        }
        if (Skill1.isSkill1)
        {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetBool("isSpinning", true);
        }
        if (
            skill2Pressed &&
            !Skill2.isSkill2 &&
            !isDodging &&
            !Skill1.isSkill1 &&
            isOnGround2 &&
            !BossMonster.BossIsDead &&
            PlayerLife.MP >= 15
        )
        {
            StartCoroutine(Skill2Time(0.2f));
        }

        if (
            !attackPressed &&
            !isAttacking &&
            !isDodging &&
            !Skill1.isSkill1 &&
            !Skill2.isSkill2 &&
            !BossMonster.BossIsDead
        )
        {
            if (leftPressed)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                playerAnim.SetBool("isWalking", true);
            }
            else if (rightPressed)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                playerAnim.SetBool("isWalking", true);
            }
        }

        if (isWalking && !leftPressed && !rightPressed)
        {
            playerAnim.SetBool("isWalking", false);
        }
        if (isJumping && !jumpPressed)
        {
            playerAnim.SetBool("isJumping", false);
        }
        if (!Skill1.isSkill1)
        {
            playerAnim.SetBool("isSpinning", false);
        }
    }

    IEnumerator AttackTime(float time)
    {
        playerAudio.PlayOneShot(attackSound, 1.0f);
        playerAnim.SetBool("isWalking", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isAttacking", true);
        yield return new WaitForSecondsRealtime(time);
        playerAnim.SetBool("isAttacking", false);
    }

    IEnumerator DodgeTime(float time)
    {
        playerAudio.PlayOneShot(dodgeSound, 1.0f);
        isDodging = true;
        isOnGround2 = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerRb.useGravity = false;
        playerRb.AddForce(transform.forward * 25, ForceMode.Impulse);

        // playerAnim.SetBool("isDodging", true);
        yield return new WaitForSecondsRealtime(time);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerRb.useGravity = true;

        // playerAnim.SetBool("isDodging", false);
        isDodging = false;
    }

    IEnumerator Skill2Time(float time)
    {
        isOnGround2 = false;
        playerAudio.PlayOneShot(skill2Sound, 1.0f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerRb.useGravity = false;
        playerRb.AddForce(transform.forward * 25, ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(time);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerRb.useGravity = true;
        // playerAnim.SetBool("isDodging", false);
    }

    void UpdatePotionUse()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
        }
    }

    void UpdateAbility()
    {
        if (Input.GetKey(KeyCode.U))
        {
        }

        if (Input.GetKey(KeyCode.I))
        {
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (isAttacking && other.transform.tag == "Monster")
        //{
        //    Destroy(other.gameObject);
        //}
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround2 = true;
        }

        if (
            other.gameObject.CompareTag("HP") ||
            other.gameObject.CompareTag("MP")
        )
        {
            playerAudio.PlayOneShot (potionSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        //if (other.gameObject.CompareTag("FireBall"))
        //{
        //    PlayerLife.HP -= 5;
        //}
    }
}
