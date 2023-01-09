using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nv_spawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] float spawnInterval;
    [SerializeField] Vector2 baseRangeSpawnAmountPerPoint;

    [SerializeField] GameObject walkerPrefab;
    [SerializeField] List<GameManager> specialEntitiesPrefabs;

    [SerializeField] int maxEnemiesAmount;

    int currentEnemiesAmount;

    int currentSpawnIteraion;

    public void OnEnemyDeath() // called from.. gamemanager.. because ... update score. .  yea. . . . .
    {
        currentEnemiesAmount--;
    }

    private void OnEnable() // enabled by GameManager on harvest time
    {
        currentEnemiesAmount = 30; // roughly amount of enemies in initial spawn container after harvest time

        StartCoroutine(SpawnIntervalRoutine());
    }

    private void SpawnEnemies()
    {
        if(currentEnemiesAmount > maxEnemiesAmount)
        {
            return;
        }

        currentSpawnIteraion++;

        foreach (Transform spawnPoint in spawnPoints)
        {
            int spawnAmount = Random.Range((int)baseRangeSpawnAmountPerPoint.x, (int)baseRangeSpawnAmountPerPoint.y);
            spawnAmount = Mathf.RoundToInt(spawnAmount * (1 + (currentSpawnIteraion * 0.2f)));

            print("SPAWN AMOUNT: " + spawnAmount);

            for(int i = 0; i < spawnAmount; i++)
            {
                Instantiate(walkerPrefab, spawnPoint.position, Quaternion.identity);
                currentEnemiesAmount++;
            }

            // handle special entities
            //float randomNum = Random.Range(0, 1f);
            //if(randomNum < 0.1f)
            //{
            //    int randomIDX = Random.Range(0, specialEntitiesPrefabs.Count);

            //    Instantiate(specialEntitiesPrefabs[randomIDX], spawnPoint.position, Quaternion.identity);
            //}
        }
    }
    
    private IEnumerator SpawnIntervalRoutine()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnEnemies();
    }
}
