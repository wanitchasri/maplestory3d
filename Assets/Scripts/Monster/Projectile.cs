using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 0.02f;

    public float rotateSpeed = 200f;
    public float MoveSpeed5 = 1f;

    private AudioSource projectileAudio;
    public AudioClip hitSound;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        projectileAudio = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameActive)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 5.0f * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.transform.tag == "BossMonster")
        //{
        //    Destroy(gameObject);
        //}

        if (other.transform.tag == "Player")
        {
            PlayerLife.HP -= 10;
            PlayerController.isAttackedByBoss = true;
            projectileAudio.PlayOneShot(hitSound, 5.0f);
            
            Destroy(gameObject);
        }

        if (other.transform.tag == "Ground")
        {
            Debug.Log("Hit the ground!");
            Destroy(gameObject);
        }

        
    }
}

