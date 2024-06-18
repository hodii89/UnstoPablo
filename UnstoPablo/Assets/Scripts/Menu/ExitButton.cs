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
        // SprawdŸ, czy mamy komponent AudioSource na tym obiekcie
        if (audioSource != null && ClickingSound != null)
        {
            // Odtwórz dŸwiêk œmierci przez audioSource
            audioSource.PlayOneShot(ClickingSound);
        }

        Application.Quit();
        Debug.Log("Exited");
    }
}
