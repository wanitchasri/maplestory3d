using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_TestBoss : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;

    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;
    public ParticleSystem explosion;
    public AudioClip hitSound;
    private AudioSource playerAudio;

    public bool ableToMakeADoubleJump = true;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);

        if (isGrounded)
        {
            direction.y = -1;
            ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(projectilePrefab1, transform.position, projectilePrefab1.transform.rotation);
                Instantiate(projectilePrefab2, transform.position, projectilePrefab2.transform.rotation);
            }

        }
        else
        {
            direction.y += gravity * Time.deltaTime;//Add Gravity
            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }
        controller.Move(direction * Time.deltaTime);
    }
    private void DoubleJump()
    {
        //Double Jump
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }
    private void Jump()
    {
        //Jump
        direction.y = jumpForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // isOnGround = true;
        if (collision.gameObject.CompareTag("FireBall"))
        {
            PlayerManager_TestBoss.currentHealth -= 10;
            explosion.Play();
            playerAudio.PlayOneShot(hitSound, 1.0f);
        }
    }
}
