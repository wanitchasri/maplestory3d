using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Test : MonoBehaviour
{
    public static bool hit;

    private bool attack;

    public AudioClip hitSound;

    private AudioSource playerAudio;

    public ParticleSystem blood;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    IEnumerator AttackTime(float time)
    {
        hit = false;
        attack = true;
        yield return new WaitForSecondsRealtime(time);
        attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.J) &&
            !attack &&
            !PlayerController.isDodging
        )
        {
            StartCoroutine(AttackTime(0.5f));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (attack && other.transform.tag == "Monster" && !hit)
        {
            blood.Play();
            hit = true;
            playerAudio.PlayOneShot(hitSound, 1.0f);

            // Destroy(other.gameObject);
            // Vector3 dir = other.transform.position - transform.position;
            // other.transform.LookAt(transform.position);
            other.transform.Translate(-Vector3.forward * 0.5f);
            // coin++;
        }
    }
}
