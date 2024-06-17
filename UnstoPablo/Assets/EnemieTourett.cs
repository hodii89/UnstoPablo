using System.Collections;
using UnityEngine;

public class EnemieTourett : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab pocisku do spawnowania
    public float spawnCooldown = 2f; // Czas w sekundach pomiêdzy spawnami

    // Start jest wywo³ywana przed pierwsz¹ klatk¹ Update
    void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    IEnumerator SpawnProjectiles()
    {
        while (true) // Nieskoñczona pêtla
        {
            SpawnProjectile();
            yield return new WaitForSeconds(spawnCooldown); // Czekaj przez czas spawnCooldown
        }
    }

    void SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}