using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ClickingSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MenuClick()
    {
        // Sprawd�, czy mamy komponent AudioSource na tym obiekcie
        if (audioSource != null && ClickingSound != null)
        {
            // Odtw�rz d�wi�k klikni�cia przez audioSource
            audioSource.PlayOneShot(ClickingSound);
        }
         Debug.Log("scena z przed załadowana");
        // Wywo�aj metod� DelayedSceneLoad po 0.3 sekundy
        SceneManager.LoadScene(0);
    }


}
