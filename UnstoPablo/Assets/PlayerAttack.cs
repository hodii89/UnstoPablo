using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Reference to the PlayerMove script
    private playerMove playerMove;

    // Zmienna przechowująca obrażenia zadawane przez kontakt
    public int contactDamage = 10;

    void Start()
    {
        // Find the PlayerMove component on the same GameObject
        playerMove = GetComponent<playerMove>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if dashing and object has the "Enemy" tag
        if (playerMove.dashing && collision.gameObject.tag == "Enemie")
        {
            // Pobranie komponentu UniversalHealth z obiektu, z którym nastąpiła kolizja
            HealthUniversal enemyHealth = collision.gameObject.GetComponent<HealthUniversal>();

            // Sprawdzenie, czy obiekt posiada komponent UniversalHealth
            if (enemyHealth != null)
            {
                // Zadanie obrażeń obiektowi
                enemyHealth.SubtractHealth(contactDamage);
            }
        }
    }
}