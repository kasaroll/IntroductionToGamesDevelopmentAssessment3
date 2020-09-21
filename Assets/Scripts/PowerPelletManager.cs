using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPelletManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().isPlaying == true && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().loop == false)
        {
            gameObject.GetComponent<Animator>().enabled = false;
        } else
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
