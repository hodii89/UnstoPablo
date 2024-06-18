using UnityEngine;
using System.Collections;

public class UniversalAttack : MonoBehaviour
{
    public string victimTag;
    public int cyclicDamage;
    public float cyclicCooldown;
    private bool didTouch;
    private bool isTouchingAny;
    private ScoreCountingScript scoreCountingScript;

    private void Start()
    {
        // Find the ScoreCountingScript in the scene
        scoreCountingScript = FindObjectOfType<ScoreCountingScript>();

        // Check if scoreCountingScript is found
        if (scoreCountingScript == null)
        {
            Debug.LogError("ScoreCountingScript not found in the scene.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(victimTag) && !didTouch)
        {
            HealthUniversal enemyHealth = collision.collider.GetComponent<HealthUniversal>();
            Debug.Log("enemy hit");

            if (enemyHealth != null)
            {
                didTouch = true;
                StartCoroutine(CyclicDamage(enemyHealth));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(victimTag))
        {
            didTouch = false;
        }
    }

    IEnumerator CyclicDamage(HealthUniversal enemyHealth)
    {
        while (didTouch)
        {
            enemyHealth.SubtractHealth(cyclicDamage);
            yield return new WaitForSeconds(cyclicCooldown);
        }
    }
}