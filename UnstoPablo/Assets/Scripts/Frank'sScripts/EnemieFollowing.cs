using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    public string targetTag = "Player"; // Tag obiektu, za kt�rym ma pod��a�
    public float speed = 5f; // Pr�dko�� poruszania si�
    public float stoppingDistance = 1f; // Dystans, na jakim obiekt si� zatrzymuje
    public bool isFollowing;
    public bool isReachingPermanent;

    private bool didReachedTarget;

    public enum ReactionMode { Stop, AutoDestruction }
    public ReactionMode reactionMode = ReactionMode.Stop; // Tryb reakcji po osi�gni�ciu stoppingDistance

    private Transform target; // Transform celu
    private Rigidbody rb; // Rigidbody tego obiektu

    void Start()
    {
        // Znajd� obiekt z okre�lonym tagiem
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Nie znaleziono obiektu z tagiem: " + targetTag);
        }

        // Pobierz komponent Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Brak komponentu Rigidbody na obiekcie.");
        }
    }

    void FixedUpdate()
    {
        if (isFollowing && target != null)
        {
            // Oblicz kierunek do celu na bie��co
            Vector3 direction = (target.position - transform.position).normalized;
            transform.forward = direction; // Koryguj kierunek obiektu
        }

        if (target != null && rb != null)
        {
            // Oblicz kierunek do celu
            Vector3 direction = (target.position - transform.position).normalized;

            // Sprawd�, czy odleg�o�� do celu jest wi�ksza ni� dystans zatrzymania
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance > stoppingDistance)
            {
                // Porusz obiekt w kierunku celu
                rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            }
            else
            {
                HandleReaction();
            }
        }
    }

    void HandleReaction()
    {
        didReachedTarget = true;



        switch (reactionMode)
        {
            case ReactionMode.Stop:
                // Nic nie r�b, obiekt si� zatrzymuje
                break;

            case ReactionMode.AutoDestruction:
                // Zniszcz obiekt
                Destroy(gameObject);
                break;
        }
    }
}