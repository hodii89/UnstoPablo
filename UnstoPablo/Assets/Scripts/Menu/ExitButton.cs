using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ClickingSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ExitGame()
    {
        // Sprawd�, czy mamy komponent AudioSource na tym obiekcie
        if (audioSource != null && ClickingSound != null)
        {
            // Odtw�rz d�wi�k �mierci przez audioSource
            audioSource.PlayOneShot(ClickingSound);
        }

        Application.Quit();
        Debug.Log("Exited");
    }
}
