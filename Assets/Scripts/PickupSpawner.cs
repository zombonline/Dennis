using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Pickups")]
    [SerializeField] GameObject pointPickup;
    [SerializeField] GameObject[] craftableParts;

    [Header("Spawn Rate")]
    [SerializeField] float pointSpawnRateMin;
    [SerializeField] float pointSpawnRateMax;
    [SerializeField] float partSpawnRateMin;
    [SerializeField] float partSpawnRateMax;

    [Header("Spawn Boundaries")]
    [Tooltip("Insert in order of: -X, X, -Y, Y")]
    [SerializeField] Transform[] SpawnBoundaries;

    float pointSpawnRate;
    float partSpawnRate;

    float pointSpawnTimer;
    float partSpawnTimer;

    private void Start()
    {
        pointSpawnRate = Random.Range(pointSpawnRateMin, pointSpawnRateMax);
        partSpawnRate = Random.Range(partSpawnRateMin, partSpawnRateMax);
    }

    private void Update()
    {
        pointSpawnTimer += Time.deltaTime;
        partSpawnTimer += Time.deltaTime;

        if(pointSpawnTimer > pointSpawnRate)
        {
            SpawnPoint();
            pointSpawnTimer = 0;
            pointSpawnRate = Random.Range(pointSpawnRateMin, pointSpawnRateMax);
        }
        if(partSpawnTimer > partSpawnRate)
        {
            SpawnPart();
            partSpawnTimer = 0;
            partSpawnRate = Random.Range(partSpawnRateMin, partSpawnRateMax);
        }
    }

    void SpawnPart()
    {
        Instantiate(craftableParts[Random.Range(0, craftableParts.Length)], RandomSpawnPoint(), transform.rotation);
    }
    void SpawnPoint()
    {
        Instantiate(pointPickup, RandomSpawnPoint(), transform.rotation);
    }

    Vector2 RandomSpawnPoint()
    {
        Vector2 randomSpawnPoint = new Vector2
            (Random.Range(SpawnBoundaries[0].position.x, SpawnBoundaries[1].position.x),
            Random.Range(SpawnBoundaries[2].position.y, SpawnBoundaries[3].position.y));
        return randomSpawnPoint;

    }

}
