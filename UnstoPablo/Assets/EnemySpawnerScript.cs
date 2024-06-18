using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerObject
{
    public GameObject spawnerContent;  // Prefab do zespawnowania
    public Transform spawnerTransform; // Transformacja, w kt�rej ma si� zespawnowa�
    public float spawnerCooldown;      // Czas pomi�dzy respawnami
    public float maxDesynchronization; // Maksymalna warto�� losowego przesuni�cia czasowego

    [HideInInspector]
    public float nextSpawnTime;        // Nast�pny czas, kiedy mo�e si� zespawnowa�
    [HideInInspector]
    public float desynchronization;    // Indywidualna warto�� losowego przesuni�cia czasowego
}

public class EnemySpawnerScript : MonoBehaviour
{
    public List<SpawnerObject> spawners; // Lista obiekt�w SpawnerObject
    public float spawnRadius = 5f; // Promie� w jakim mog� pojawia� si� przeciwnicy od transformacji

    void Start()
    {
        // Inicjalizacja nast�pnego czasu spawn�w na pocz�tek z losowym przesuni�ciem
        foreach (SpawnerObject spawner in spawners)
        {
            spawner.desynchronization = Random.Range(0, spawner.maxDesynchronization);
            spawner.nextSpawnTime = Time.time + spawner.spawnerCooldown + spawner.desynchronization;
        }
    }

    void Update()
    {
        // Sprawdzanie ka�dego spawnerObject czy ju� czas na spawn
        foreach (SpawnerObject spawner in spawners)
        {
            if (Time.time >= spawner.nextSpawnTime)
            {
                SpawnEnemy(spawner);
                spawner.nextSpawnTime = Time.time + spawner.spawnerCooldown + spawner.desynchronization; // Ustawienie nast�pnego czasu spawnu z indywidualnym losowym przesuni�ciem
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