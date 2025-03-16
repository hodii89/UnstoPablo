using UnityEngine;

public class HealthUniversal : MonoBehaviour
{
    // Zmienna przechowuj�ca zdrowie
    public int health = 100;
    public int deathPoints;

    private ScoreCountingScript scoreCountingScript;
    private AudioSource audioSource;

    // Zmienna do przechowywania klipu d�wi�kowego �mierci
    public AudioClip deathSound;

    private void Start()
    {
        // Find the ScoreCountingScript in the scene
        scoreCountingScript = FindObjectOfType<ScoreCountingScript>();

        // Przypisz komponent AudioSource znajduj�cy si� na tym samym obiekcie
        audioSource = GetComponent<AudioSource>();
    }

    // Funkcja do dodawania zdrowia
    public void AddHealth(int amount)
    {
        health += amount;
        // Upewnij si�, �e zdrowie nie przekroczy maksymalnej warto�ci, np. 100
        health = Mathf.Min(health, 100);
      //  Debug.Log("Zdrowie dodane: " + amount + ". Aktualne zdrowie: " + health);
    }

    // Funkcja do odejmowania zdrowia
    public void SubtractHealth(int amount)
    {
        health -= amount;
        // Upewnij si�, �e zdrowie nie spadnie poni�ej 0
        health = Mathf.Max(health, 0);
//        Debug.Log("Zdrowie odj�te: " + amount + ". Aktualne zdrowie: " + health + " dla: " + gameObject.name);

        // Sprawd�, czy zdrowie spad�o poni�ej 0 i wywo�aj Die()
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Sprawd�, czy mamy komponent AudioSource na tym obiekcie
        if (deathSound != null)
        {
            // Spawnowanie nowego obiektu na pozycji obecnego obiektu
            GameObject soundObject = GameObject.Instantiate(new GameObject(), transform.position, Quaternion.identity);

            // Dodawanie komponentu AudioSource do nowo stworzonego obiektu
            AudioSource soundSource = soundObject.AddComponent<AudioSource>();

            // Ustawianie clipu d�wi�ku �mierci na komponencie AudioSource
            soundSource.clip = deathSound;

            // Odtwarzanie d�wi�ku �mierci
            soundSource.Play();
        }
        else
        {
           // Debug.LogWarning("Brak komponentu AudioSource lub klipu d�wi�kowego �mierci na obiekcie " + gameObject.name);
        }

        // Dodaj punkty do licznika
        scoreCountingScript.points += deathPoints;

        // Zniszczenie obiektu HealthUniversal
        Destroy(gameObject);
    }
}