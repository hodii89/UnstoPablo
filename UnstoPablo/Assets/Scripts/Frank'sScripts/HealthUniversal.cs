using Unity.VisualScripting;
using UnityEngine;

public class HealthUniversal : MonoBehaviour
{
    // Zmienna przechowuj�ca zdrowie
    public int health = 100;

    // Funkcja do dodawania zdrowia
    public void AddHealth(int amount)
    {
        health += amount;
        // Upewnij si�, �e zdrowie nie przekroczy maksymalnej warto�ci, np. 100
        health = Mathf.Min(health, 100);
        Debug.Log("Zdrowie dodane: " + amount + ". Aktualne zdrowie: " + health);
    }

    // Funkcja do odejmowania zdrowia
    public void SubtractHealth(int amount)
    {
        health -= amount;
        // Upewnij si�, �e zdrowie nie spadnie poni�ej 0
        health = Mathf.Max(health, 0);
        Debug.Log("Zdrowie odj�te: " + amount + ". Aktualne zdrowie: " + health);
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
        Destroy(gameObject); // Zniszczenie obiektu
    }
}