using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float forwardBound = 60;
    private float backBound = -39;
    private float downBound = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Create to destroy the object in the game
        if (transform.position.x > forwardBound)
        {
            Destroy(gameObject);
        }

        // Create to destroy the object in the game
        if (transform.position.x < backBound)
        {
            Destroy(gameObject);
        }

        // Create to destroy the object in the game
        if (transform.position.y < downBound)
        {
            Destroy(gameObject);
        }
    }
}