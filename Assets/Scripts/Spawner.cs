using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileWarning;
    float yMin = -10;
    float yMax = 10;
    float xMin = -10;
    float xMax = 10;

    float startDelay = 3f;
    [SerializeField] public bool canSpawn = true;

    [SerializeField] float beginningMinSpawnTime;
    [SerializeField] float absoluteMinSpawnTime;
    [SerializeField] float beginningMaxSpawnTime;
    [SerializeField] float absoluteMaxSpawnTime;
    [SerializeField] float spawnIncreaseTime;
    [SerializeField] float spawnIncreaseRate;
    
    float minSpawnTime;
    float maxSpawnTime;
    float spawnIncreaseTimer = 0;

    private void Start()
    {
        minSpawnTime = beginningMinSpawnTime;
        maxSpawnTime = beginningMaxSpawnTime;

        if (tag == "Y Spawner")
        {
            Invoke("SpawnProjectileY", startDelay);
        }
        if (tag == "X Spawner")
        {
            Invoke("SpawnProjectileX", startDelay);
        }
    }

    private void Update()
    {
        DecreaseSpawnRate();
    }


    void SpawnProjectileY()
    {
        if (canSpawn)
        {
            var randomSpawnPoint = new Vector2(transform.position.x, Random.Range(yMin, yMax));

            Instantiate(projectileWarning, randomSpawnPoint, transform.rotation);
            Instantiate(projectile, randomSpawnPoint, transform.rotation);
        }
        Invoke("SpawnProjectileY", Random.Range(minSpawnTime, maxSpawnTime));
    }
    void SpawnProjectileX()
    {
        if (canSpawn)
        {
            var randomSpawnPoint = new Vector2(Random.Range(xMin, xMax), transform.position.y);

            Instantiate(projectileWarning, randomSpawnPoint, transform.rotation);
            Instantiate(projectile, randomSpawnPoint, transform.rotation);
        }
        Invoke("SpawnProjectileX", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void DecreaseSpawnRate()
    {
        
        spawnIncreaseTimer += Time.deltaTime;

        if(spawnIncreaseTimer > spawnIncreaseTime)
        {
            if (minSpawnTime > absoluteMinSpawnTime)
            {
                minSpawnTime -= spawnIncreaseRate;
                maxSpawnTime -= spawnIncreaseRate;
            }
            else if (maxSpawnTime > absoluteMaxSpawnTime)
            {
                maxSpawnTime -= spawnIncreaseRate;
            }
            else
            {
                minSpawnTime = absoluteMinSpawnTime;
                maxSpawnTime = absoluteMaxSpawnTime;
                return;
            }
            spawnIncreaseTimer = 0;
        }
    }
    public void ResetSpawnRate()
    {
        minSpawnTime = beginningMinSpawnTime;
        maxSpawnTime = beginningMaxSpawnTime;
    }


}
