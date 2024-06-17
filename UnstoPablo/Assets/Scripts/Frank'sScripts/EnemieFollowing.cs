using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    public string targetTag = "Player"; // Tag obiektu, za którym ma pod¹¿aæ
    public float speed = 5f; // Prêdkoœæ poruszania siê
    public bool doesDistanceMatter;
    public float stoppingDistance = 1f; // Dystans, na jakim obiekt siê zatrzymuje
    public bool doesTouchingMatter;
    public bool isFollowing;
    public bool isReachingPermanent;

    private bool didReachedTarget;
    private bool didTouched;

    public enum ReactionMode { Stop, AutoDestruction }
    public ReactionMode reactionMode = ReactionMode.Stop; // Tryb reakcji po osi¹gniêciu stoppingDistance

    private Transform target; // Transform celu
    private Rigidbody rb; // Rigidbody tego obiektu
    private Vector3 moveDirection; // Kierunek poruszania siê

    public void Awake()
    {
        didTouched = false;
    }
    void Start()
    {
        FindTarget(); // ZnajdŸ obiekt z okreœlonym tagiem
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
            // Oblicz kierunek do celu na bie¿¹co
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
                        FindTarget(); // ZnajdŸ obiekt z okreœlonym tagiem
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
                FindTarget(); // ZnajdŸ obiekt z okreœlonym tagiem
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
                // Nic nie rób, obiekt siê zatrzymuje
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
            Debug.LogError("Nie znaleziono obiektów z tagiem: " + targetTag);
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