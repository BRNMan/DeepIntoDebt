using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("isSoundOn") == "true") {
            audioSource = this.GetComponent<AudioSource>();
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
