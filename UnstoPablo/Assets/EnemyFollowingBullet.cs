using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowingBullet : MonoBehaviour
{
    public string targetTag = "Player"; // Tag obiektu, za kt�rym ma pod��a�
    public float speed = 5f; // Pr�dko�� poruszania si�
    public bool doesDistanceMatter;
    public float stoppingDistance = 1f; // Dystans, na jakim obiekt si� zatrzymuje
    public bool doesTouchingMatter;
    public bool isFollowing;
    public bool isReachingPermanent;

    private bool didReachedTarget;
    private bool didTouched;

    public enum ReactionMode { Stop, AutoDestruction }
    public ReactionMode reactionMode = ReactionMode.Stop; // Tryb reakcji po osi�gni�ciu stoppingDistance

    private Transform target; // Transform celu
    private Rigidbody rb; // Rigidbody tego obiektu
    private Vector3 moveDirection; // Kierunek poruszania si�

    private AudioSource audioSource;

    // Zmienna do przechowywania klipu d�wi�kowego �mierci
    public AudioClip shootingSound;

    public void Awake()
    {
        didTouched = false;
    }
    void Start()
    {
        // Przypisz komponent AudioSource znajduj�cy si� na tym samym obiekcie
        audioSource = GetComponent<AudioSource>();

        // Sprawd�, czy mamy komponent AudioSource na tym obiekcie
        if (audioSource != null && shootingSound != null)
        {
            // Odtw�rz d�wi�k �mierci przez audioSource
            audioSource.PlayOneShot(shootingSound);
        }

        FindTarget(); // Znajd� obiekt z okre�lonym tagiem
        CalculateMoveDirection();

        // Pobierz komponent Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Brak komponentu Rigidbody na obiekcie.");
        }
    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            // Oblicz kierunek do celu na bie��co
            CalculateMoveDirection();
        }
        if (target != null)
        {
            if (rb != null)
            {
                float distance = Vector3.Distance(target.position, transform.position);
                // Porusz obiekt w obliczonym kierunku
                rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
                if (isReachingPermanent == false && didReachedTarget)
                {
                    didReachedTarget = false;
                    FindTarget(); // Znajd� obiekt z okre�lonym tagiem
                    CalculateMoveDirection();
                }
                else if ((distance <= stoppingDistance && doesDistanceMatter) || (didTouched && doesTouchingMatter))
                {
                    HandleReaction();
                }
            }
        }
        else
        {
            if (!isFollowing)
            {
                Destroy(gameObject);
            }
        }
    }
    void CalculateMoveDirection()
    {
        if (target != null)
        {
            // Oblicz kierunek do celu
            moveDirection = (target.position - transform.position).normalized;
        }
        else
        {
            if (isFollowing)
            {
                FindTarget(); // Znajd� obiekt z okre�lonym tagiem
                CalculateMoveDirection();
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

    void FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;

            foreach (GameObject obj in targets)
            {
                float distance = Vector3.Distance(obj.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = obj.transform;
                }
            }

            if (closestTarget != null)
            {
                target = closestTarget;
            }
        }
        else
        {
           // Debug.LogError("Did not find an object with tag: " + targetTag);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(targetTag))
        {
            didTouched = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(targetTag))
        {
            didTouched = false;
        }
    }
}