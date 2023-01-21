using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class wv_manager : MonoBehaviour
{
    [SerializeField] wv_spawner spawner;

    [SerializeField] wv_wave_list[] regularWavesArr;
    [SerializeField] wv_wave_list[] loopingWavesArr;

    [SerializeField] TextMeshPro waveCounterText, enemiesCounterText;
    [SerializeField] GameObject waveCompleteTextWrapper;

    // these values now reside in pd_sesion ScriptableObject instance
    //int currentWaveIDXRegular = 0;
    //int currentWaveIDXLooping = -1;
    //int currentWaveCounter = 1; // represents total amount of waves that have passed - does, as opposed to ints above, NOT represent any kind of list index

    int remainingEnemiesCounter;

    public void InitWave()
    {
        waveCounterText.text = "WAVE " + g_refs.Instance.sessionData.currentWaveTotal;

        wv_wave waveToInit = GetWaveFromList();

        remainingEnemiesCounter = waveToInit.AmountOfEnemies();
        enemiesCounterText.text = "REMAINING ENEMIES: " + remainingEnemiesCounter;

        spawner.InitNewWave(waveToInit);
    }

    private wv_wave GetWaveFromList()
    {
        wv_wave[] possibleWaves = g_refs.Instance.sessionData.currentWaveLooping < 0 ? regularWavesArr[g_refs.Instance.sessionData.currentWaveRegular].possibleWaves : loopingWavesArr[g_refs.Instance.sessionData.currentWaveLooping].possibleWaves;
        int randomIDX = Random.Range(0, possibleWaves.Length - 1);
        return possibleWaves[randomIDX];
    }

    private void AdvanceWaveIDX()
    {
        if (g_refs.Instance.sessionData.currentWaveLooping < 0)
        {
            g_refs.Instance.sessionData.currentWaveRegular++;
            if (g_refs.Instance.sessionData.currentWaveRegular == regularWavesArr.Length)
            {
                //currentWaveLooping = 0;
                g_refs.Instance.sessionData.currentWaveLooping = Random.Range(0, loopingWavesArr.Length - 1);
            }
        }
        else
        {
            g_refs.Instance.sessionData.currentWaveLooping++;
            if (g_refs.Instance.sessionData.currentWaveLooping == loopingWavesArr.Length)
            {
                g_refs.Instance.sessionData.currentWaveLooping = 0;
            }
        }
    }

    public void HandleEnemyDeath() // called from en_health_base through g_refs singleton instance
    {
        remainingEnemiesCounter--;
        enemiesCounterText.text = "REMAINING ENEMIES: " + remainingEnemiesCounter;
        if (remainingEnemiesCounter <= 0)
        {
            StartCoroutine(ExitWave());
        }
    }

    private IEnumerator ExitWave()
    {
        g_refs.Instance.sessionData.currentWaveTotal++;
        AdvanceWaveIDX();

        waveCompleteTextWrapper.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

[System.Serializable]
public class wv_wave_list // wrapper class to serialize nested arrays in unity inspector
{
    public wv_wave[] possibleWaves;
}
