using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skill1 : MonoBehaviour
{
    public static bool hit;

    public static bool isSkill1 = false;

    public AudioClip skill1Sound;

    public AudioClip hitSound;

    private AudioSource playerAudio;

    public TextMeshProUGUI skill1Text;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.U) &&
            !isSkill1 &&
            !Skill2.isSkill2 &&
            !PlayerController.isDodging &&
            PlayerLife.MP > 25 &&
            !BossMonster.BossIsDead
        )
        {
            StartCoroutine(Skill1Time(0.2f));
        }
    }

    IEnumerator Skill1Time(float time)
    {
        isSkill1 = true;
        PlayerLife.MP -= 25;

        skill1Text.gameObject.SetActive(true);
        skill1Text.text = "Tornado Spin !";
        skill1Text.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        
        for (int i = 0; i < 3; i++)
        {
            hit = false;
            playerAudio.PlayOneShot(skill1Sound, 1.0f);
            yield return new WaitForSecondsRealtime(time);
        }
        skill1Text.gameObject.SetActive(false);
        isSkill1 = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSkill1 && other.transform.tag == "Monster" && !hit)
        {
            Monster.getsSkill1 = true;
            hit = true;
            playerAudio.PlayOneShot(hitSound, 1.0f);
            other.transform.Translate(-Vector3.forward * 0.5f);
        }

        if (isSkill1 && other.transform.tag == "Boss" && !hit)
        {
            BossMonster.getsSkill1 = true;
            hit = true;
            playerAudio.PlayOneShot(hitSound, 1.0f);
        }
    }
}
