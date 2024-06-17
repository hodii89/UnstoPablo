using UnityEngine;
using System.Collections;

public class UniversalAttack : MonoBehaviour
{
    public string victimTag;
    public int cyclicDamage;
    public float cyclicCooldown;
    private bool didTouch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(victimTag) && didTouch == false)
        {
            HealthUniversal enemyHealth = other.GetComponent<HealthUniversal>();

            if (enemyHealth != null)
            {
                didTouch = true;
                StartCoroutine(CyclicDamage(enemyHealth));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(victimTag))
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
    private void OnEnable()
    {
        didTouch = true;
    }
}