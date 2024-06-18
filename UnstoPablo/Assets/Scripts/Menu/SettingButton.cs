using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ClickingSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
