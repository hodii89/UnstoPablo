using Unity.VisualScripting;
using UnityEngine;

public class HealthUniversal : MonoBehaviour
{
    // Zmienna przechowuj¹ca zdrowie
    public int health = 100;
    public int deathPoints;

    private ScoreCountingScript scoreCountingScript;

    private void Start()
    {
        // Find the ScoreCountingScript in the scene
        scoreCountingScript = FindObjectOfType<ScoreCountingScript>();
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
        Debug.Log("Zdrowie odjête: " + amount + ". Aktualne zdrowie: " + health + "dla: " + gameObject.name);
    }

    public void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
            scoreCountingScript.points += deathPoints;
            Destroy(gameObject); // Zniszczenie obiektu
    }
}