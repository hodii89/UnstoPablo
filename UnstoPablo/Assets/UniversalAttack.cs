using UnityEngine;
using System.Collections;

public class UniversalAttack : MonoBehaviour
{
    public string victimTag;
    public int cyclicDamage;
    public float cyclicCooldown;
    [SerializeField] private bool didTouch;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(victimTag) && didTouch == false)
        {
            HealthUniversal enemyHealth = collision.collider.GetComponent<HealthUniversal>();

            if (enemyHealth != null)
            {
                Debug.Log(collision.collider.name);
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