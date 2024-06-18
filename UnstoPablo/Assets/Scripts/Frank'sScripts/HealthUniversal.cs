using UnityEngine;

public class HealthUniversal : MonoBehaviour
{
    // Zmienna przechowuj¹ca zdrowie
    public int health = 100;
    public int deathPoints;

    private ScoreCountingScript scoreCountingScript;
    private AudioSource audioSource;

    // Zmienna do przechowywania klipu dŸwiêkowego œmierci
    public AudioClip deathSound;

    private void Start()
    {
        // Find the ScoreCountingScript in the scene
        scoreCountingScript = FindObjectOfType<ScoreCountingScript>();

        // Przypisz komponent AudioSource znajduj¹cy siê na tym samym obiekcie
        audioSource = GetComponent<AudioSource>();
    }

    // Funkcja do dodawania zdrowia
    public void AddHealth(int amount)
    {
        health += amount;
        // Upewnij siê, ¿e zdrowie nie przekroczy maksymalnej wartoœci, np. 100
        health = Mathf.Min(health, 100);
        Debug.Log("Zdrowie dodane: " + amount + ". Aktualne zdrowie: " + health);
    }

    // Funkcja do odejmowania zdrowia
    public void SubtractHealth(int amount)
    {
        health -= amount;
        // Upewnij siê, ¿e zdrowie nie spadnie poni¿ej 0
        health = Mathf.Max(health, 0);
        Debug.Log("Zdrowie odjête: " + amount + ". Aktualne zdrowie: " + health + " dla: " + gameObject.name);

        // SprawdŸ, czy zdrowie spad³o poni¿ej 0 i wywo³aj Die()
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // SprawdŸ, czy mamy komponent AudioSource na tym obiekcie
        if (deathSound != null)
        {
            // Spawnowanie nowego obiektu na pozycji obecnego obiektu
            GameObject soundObject = GameObject.Instantiate(new GameObject(), transform.position, Quaternion.identity);

            // Dodawanie komponentu AudioSource do nowo stworzonego obiektu
            AudioSource soundSource = soundObject.AddComponent<AudioSource>();

            // Ustawianie clipu dŸwiêku œmierci na komponencie AudioSource
            soundSource.clip = deathSound;

            // Odtwarzanie dŸwiêku œmierci
            soundSource.Play();
        }
        else
        {
            Debug.LogWarning("Brak komponentu AudioSource lub klipu dŸwiêkowego œmierci na obiekcie " + gameObject.name);
        }

        // Dodaj punkty do licznika
        scoreCountingScript.points += deathPoints;

        // Zniszczenie obiektu HealthUniversal
        Destroy(gameObject);
    }
}