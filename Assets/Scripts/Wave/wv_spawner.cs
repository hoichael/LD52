using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class wv_spawner : MonoBehaviour
{
    [SerializeField] lv_pool pool;
    [SerializeField] GameObject walker, walkerElite, shooter, shooterElite, exploder, exploderElite, runner, runnerElite, floater, floaterElite, giant;
    Dictionary<en_type, GameObject> enDict;
    wv_wave currentWave;
    int currentSubwaveIDX;

    private void Awake()
    {
        PopulateEnemyDict();
    }

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
                Instantiate(enDict[enemyInfo.type], spawnPointsArr[spawnPosIDX].position, Quaternion.identity);
                //pool.Dispatch(
                //    enemyInfo.type,
                //    spawnPointsArr[spawnPosIDX].position, 
                //    Quaternion.identity);
            }
        }

        currentSubwaveIDX++;
        if(currentSubwaveIDX < currentWave.subwavesArr.Length)
        {
            StartCoroutine(HandleSubwaveDelay());
        }
    }

    private void PopulateEnemyDict()
    {
        enDict = new Dictionary<en_type, GameObject>();
        enDict.Add(en_type.walker, walker);
        enDict.Add(en_type.walker_elite, walkerElite);
        enDict.Add(en_type.shooter, shooter);
        enDict.Add(en_type.shooter_elite, shooterElite);
        enDict.Add(en_type.exploder, exploder);
        enDict.Add(en_type.exploder_elite, exploderElite);
        //enDict.Add(en_type.runner, runner);
        //enDict.Add(en_type.runner_elite, runnerElite);
        //enDict.Add(en_type.floater, floater);
        //enDict.Add(en_type.floater_elite, floaterElite);
        enDict.Add(en_type.giant, giant);
    }
}

public enum en_type
{
    walker,
    walker_elite,
    shooter,
    shooter_elite,
    exploder,
    exploder_elite,
    runner,
    runner_elite,
    floater,
    floater_elite,
    giant
}
