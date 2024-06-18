using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ClickingSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStart()
    {
        // Sprawd�, czy mamy komponent AudioSource na tym obiekcie
        if (audioSource != null && ClickingSound != null)
        {
            // Odtw�rz d�wi�k klikni�cia przez audioSource
            audioSource.PlayOneShot(ClickingSound);
        }

        // Wywo�aj metod� DelayedSceneLoad po 0.3 sekundy
        Invoke("DelayedSceneLoad", 0.3f);
    }

    private void DelayedSceneLoad()
    {
        // Za�aduj nast�pn� scen�
        SceneManager.LoadScene(1);
    }
}