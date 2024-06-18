using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ClickingSound;
    public GameObject SetWin;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetWin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(SetWin == true)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SetWin.SetActive(false);
            }
        }
       
    }
    public void OpenSet()
    {
        Debug.Log("Settings");
        SetWin.SetActive(true);
    }
}
