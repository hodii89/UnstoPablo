using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerObject
{
    public GameObject spawnerContent;  // Prefab do zespawnowania
    public Transform spawnerTransform; // Transformacja, w której ma siê zespawnowaæ
    public float spawnerCooldown;      // Czas pomiêdzy respawnami
    public float maxDesynchronization; // Maksymalna wartoœæ losowego przesuniêcia czasowego

    [HideInInspector]
    public float nextSpawnTime;        // Nastêpny czas, kiedy mo¿e siê zespawnowaæ
    [HideInInspector]
    public float desynchronization;    // Indywidualna wartoœæ losowego przesuniêcia czasowego
}

public class EnemySpawnerScript : MonoBehaviour
{
    public List<SpawnerObject> spawners; // Lista obiektów SpawnerObject
    public float spawnRadius = 5f; // Promieñ w jakim mog¹ pojawiaæ siê przeciwnicy od transformacji

    void Start()
    {
        // Inicjalizacja nastêpnego czasu spawnów na pocz¹tek z losowym przesuniêciem
        foreach (SpawnerObject spawner in spawners)
        {
            spawner.desynchronization = Random.Range(0, spawner.maxDesynchronization);
            spawner.nextSpawnTime = Time.time + spawner.spawnerCooldown + spawner.desynchronization;
        }
    }

    void Update()
    {
        // Sprawdzanie ka¿dego spawnerObject czy ju¿ czas na spawn
        foreach (SpawnerObject spawner in spawners)
        {
            if (Time.time >= spawner.nextSpawnTime)
            {
                SpawnEnemy(spawner);
                spawner.nextSpawnTime = Time.time + spawner.spawnerCooldown + spawner.desynchronization; // Ustawienie nastêpnego czasu spawnu z indywidualnym losowym przesuniêciem
            }
        }
    }

    void SpawnEnemy(SpawnerObject spawner)
    {
        if (spawner.spawnerContent != null && spawner.spawnerTransform != null)
        {
            Vector3 spawnPosition = GetRandomPositionAround(spawner.spawnerTransform.position, spawnRadius);
            Instantiate(spawner.spawnerContent, spawnPosition, spawner.spawnerTransform.rotation);
        }
    }

    Vector3 GetRandomPositionAround(Vector3 center, float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 spawnPosition = new Vector3(center.x + randomPoint.x, center.y, center.z + randomPoint.y);
        return spawnPosition;
    }
}