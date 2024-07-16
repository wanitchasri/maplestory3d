using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static bool hit;

    public static bool attack;

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
            Monster.getsHit = true;
            blood.Play();
            hit = true;
            playerAudio.PlayOneShot(hitSound, 1.0f);
            other.transform.Translate(-Vector3.forward * 0.5f);
        }

        if (attack && other.transform.tag == "Boss" && !hit)
        {
            hit = true;
            BossMonster.getsHit = true;
            playerAudio.PlayOneShot(hitSound, 1.0f);
        }
    }
}
