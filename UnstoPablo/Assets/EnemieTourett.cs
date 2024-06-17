using System.Collections;
using UnityEngine;

public class EnemieTourett : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab pocisku do spawnowania
    public float spawnCooldown = 2f; // Czas w sekundach pomi�dzy spawnami

    // Start jest wywo�ywana przed pierwsz� klatk� Update
    void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    IEnumerator SpawnProjectiles()
    {
        while (true) // Niesko�czona p�tla
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