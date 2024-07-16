using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    public float smoothSpeed = 0.15f;

    public GameManager gameManager;
    public bool cameraActivated;

    private AudioSource gameAudio;

    void Start()
    {
        cameraActivated = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameAudio = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!cameraActivated && gameManager.playerSpawned)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            offset = transform.position - target.position;
            cameraActivated = true;
        }

        if (GameManager.isInBossRoom)
        {
            gameAudio.Stop();
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
