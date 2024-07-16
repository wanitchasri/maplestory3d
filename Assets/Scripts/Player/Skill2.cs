using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skill2 : MonoBehaviour
{
    public static bool hit;

    public static bool isSkill2 = false;

    public AudioClip skill2Sound;

    private AudioSource playerAudio;

    public ParticleSystem blood;

    public TextMeshProUGUI skill2Text;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.I) &&
            !isSkill2 &&
            !Skill1.isSkill1 &&
            !PlayerController.isDodging &&
            PlayerLife.MP > 15
        )
        {
            StartCoroutine(Skill2Time(0.2f));
        }
    }

    IEnumerator Skill2Time(float time)
    {
        isSkill2 = true;

        skill2Text.gameObject.SetActive(true);
        skill2Text.text = "Ninja Dash !";
        skill2Text.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        hit = false;
        yield return new WaitForSecondsRealtime(time);
        
        isSkill2 = false;
        skill2Text.gameObject.SetActive(false);

        blood.Stop();
        PlayerLife.MP -= 15;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSkill2 && other.transform.tag == "Monster" && !hit)
        {
            Monster.getsSkill2 = true;
            blood.Play();
            hit = true;
            playerAudio.PlayOneShot(skill2Sound, 1.0f);
            other.transform.Translate(-Vector3.forward * 0.5f);
        }

        if (isSkill2 && other.transform.tag == "Boss" && !hit)
        {
            BossMonster.getsSkill2 = true;
            hit = true;
            playerAudio.PlayOneShot(skill2Sound, 1.0f);
        }
    }
}
