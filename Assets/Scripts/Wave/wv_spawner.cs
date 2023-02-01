using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class wv_spawner : MonoBehaviour
{
    [SerializeField] lv_pool pool;
    wv_wave currentWave;
    int currentSubwaveIDX;

    public void InitNewWave(wv_wave waveToInit)
    {
        currentWave = waveToInit;
        currentSubwaveIDX = 0;
        StopAllCoroutines(); // should never be necessary, but you never know. . .
        StartCoroutine(HandleSubwaveDelay());
    }

    private IEnumerator HandleSubwaveDelay()
    {
        yield return new WaitForSeconds(currentWave.subwavesArr[currentSubwaveIDX].spawnDelay);
        SpawnSubwave();
    }

    private void SpawnSubwave()
    {
        foreach (wv_subwave_enemyinfo enemyInfo in currentWave.subwavesArr[currentSubwaveIDX].enemyInfoArr)
        {
            Transform[] spawnPointsArr = currentWave.subwavesArr[currentSubwaveIDX].spawnPointsContainer.Cast<Transform>().ToArray();
            
            for (int i = 0; i < enemyInfo.amount; i++)
            {
                int spawnPosIDX = Random.Range(0, spawnPointsArr.Length);
                pool.Dispatch(
                    enemyInfo.type,
                    spawnPointsArr[spawnPosIDX].position, 
                    Quaternion.identity);
            }
        }

        currentSubwaveIDX++;
        if(currentSubwaveIDX < currentWave.subwavesArr.Length)
        {
            StartCoroutine(HandleSubwaveDelay());
        }
    }
}
