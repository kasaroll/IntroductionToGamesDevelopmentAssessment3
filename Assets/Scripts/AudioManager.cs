using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip newSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            gameObject.GetComponent<AudioSource>().clip = newSound;
            gameObject.GetComponent<AudioSource>().loop = true;
            gameObject.GetComponent<AudioSource>().Play();
        } 
    }
}
