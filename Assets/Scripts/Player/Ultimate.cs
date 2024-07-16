using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ultimate : MonoBehaviour
{
    public static bool hit;

    public static bool isUlti = false;

    public AudioClip ultiSound;

    private AudioSource playerAudio;

    public ParticleSystem fire;

    public AudioClip hitSound;

    public TextMeshProUGUI ultimateText;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.O) &&
            !isUlti &&
            !Skill1.isSkill1 &&
            !Skill2.isSkill2 &&
            !PlayerController.isDodging &&
            PlayerLife.MP > 25
        )
        {
            StartCoroutine(UltiTime(0.5f));
        }
    }

    IEnumerator UltiTime(float time)
    {
        isUlti = true;
        PlayerLife.MP -= 40;

        ultimateText.gameObject.SetActive(true);
        ultimateText.text = "Hell Fire !";
        ultimateText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        yield return new WaitForSecondsRealtime(0.5f);
        ultimateText.gameObject.SetActive(false);

        fire.Play();
        playerAudio.PlayOneShot(ultiSound, 1.0f);
        for (int i = 0; i < 20; i++)
        {
            hit = false;
            yield return new WaitForSecondsRealtime(time);
        }
        isUlti = false;
        fire.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isUlti && other.transform.tag == "Monster" && !hit)
        {
            Monster.getsSkill3 = true;
            hit = true;
            playerAudio.PlayOneShot(hitSound, 1.0f);
            other.transform.Translate(-Vector3.forward * 0.5f);
        }

        if (isUlti && other.transform.tag == "Boss" && !hit)
        {
            BossMonster.getsSkill3 = true;
            hit = true;
            playerAudio.PlayOneShot(hitSound, 1.0f);
        }
    }
}
