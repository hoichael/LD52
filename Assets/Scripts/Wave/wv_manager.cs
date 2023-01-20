using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wv_manager : MonoBehaviour
{

    [SerializeField] wv_wave_list[] regularWavesArr;
    [SerializeField] wv_wave_list[] loopingWavesArr;

    [SerializeField] TextMeshPro waveCounterText, enemiesCounterText;

    int currentWaveIDXRegular = 0;
    int currentWaveIDXLooping = -1;

    int currentWaveCounter = 1; // represents total amount of waves that have passed - does, as opposed to ints above, NOT represent any kind of list index
    int remainingEnemiesCounter;


    public void InitWave()
    {
        waveCounterText.text = "WAVE " + currentWaveCounter;

        wv_wave waveToInit = GetWaveFromList();
        remainingEnemiesCounter = waveToInit.Init(); // init func starts wave (coroutines, setup, etc...) and returns total enemy amount of wave. kinda fucky but whtv

        enemiesCounterText.text = "REMAINING ENEMIES: " + remainingEnemiesCounter;

        AdvanceWaveIDX();
    }

    private wv_wave GetWaveFromList()
    {
        wv_wave[] possibleWaves = currentWaveIDXLooping < 0 ? regularWavesArr[currentWaveIDXRegular].possibleWaves : loopingWavesArr[currentWaveIDXLooping].possibleWaves;
        int randomIDX = Random.Range(0, possibleWaves.Length - 1);
        return possibleWaves[randomIDX];
    }

    private void AdvanceWaveIDX()
    {
        if (currentWaveIDXLooping < 0)
        {
            currentWaveIDXRegular++;
            if (currentWaveIDXRegular == regularWavesArr.Length)
            {
                //currentWaveLooping = 0;
                currentWaveIDXLooping = Random.Range(0, loopingWavesArr.Length - 1);
            }
        }
        else
        {
            currentWaveIDXLooping++;
            if (currentWaveIDXLooping == loopingWavesArr.Length)
            {
                currentWaveIDXLooping = 0;
            }
        }
    }

    public void HandleEnemyDeath() // called from en_health_base through g_refs singleton instance
    {
        remainingEnemiesCounter--;
        if (remainingEnemiesCounter <= 0) ExitWave();
    }

    private void ExitWave()
    {
        print("WAVE COMPLETE!");
    }
}

[System.Serializable]
public class wv_wave_list // wrapper class to serialize nested arrays in unity inspector
{
    public wv_wave[] possibleWaves;
}
