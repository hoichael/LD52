using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wv_wave : MonoBehaviour
{
    public wv_subwave[] subwavesArr;
    public wv_wave_nv nvInfo;

    public int AmountOfEnemies()
    {
        return GetTotalEnemyAmount();
    }

    private int GetTotalEnemyAmount()
    {
        int amount = 0;

        foreach(wv_subwave subwave in subwavesArr)
        {
            foreach(wv_subwave_enemyinfo enemyInfo in subwave.enemyInfoArr)
            {
                int enemyAmount;
                if (g_refs.Instance.sessionData.loopingWaveCounter == 0)
                {
                    enemyAmount = enemyInfo.amount;
                }
                else
                {
                    enemyAmount = Mathf.RoundToInt(enemyInfo.amount * (1 + (g_refs.Instance.sessionData.loopingWaveCounter * 0.1f)));
                }

                amount += enemyAmount;
            }
        }

        return amount;
    }
}

[System.Serializable]
public class wv_wave_nv
{
    public GameObject nvContainer;
    public Material matSkybox;
}
