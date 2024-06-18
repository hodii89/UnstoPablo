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
        // SprawdŸ, czy mamy komponent AudioSource na tym obiekcie
        if (audioSource != null && ClickingSound != null)
        {
            // Odtwórz dŸwiêk klikniêcia przez audioSource
            audioSource.PlayOneShot(ClickingSound);
        }

        // Wywo³aj metodê DelayedSceneLoad po 0.3 sekundy
        Invoke("DelayedSceneLoad", 0.3f);
    }

    private void DelayedSceneLoad()
    {
        // Za³aduj nastêpn¹ scenê
        SceneManager.LoadScene(0);
    }
}
