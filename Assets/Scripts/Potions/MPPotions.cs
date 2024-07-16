using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPPotions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerLife.MP += 25;
            Destroy(this.gameObject);
        }
    }
}
