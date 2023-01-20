using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wv_wave_", menuName = "ScriptableObjects/Wave/Wave", order = 0)]
public class wv_wave : ScriptableObject
{
    [SerializeField] wv_subwave[] subwavesArr;

    public int Init() // returns enemy amount for this wave. kinda fucky but whtv
    {



        return GetTotalEnemyAmount();
    }

    private int GetTotalEnemyAmount()
    {
        int amount = 0;

        foreach(wv_subwave subwave in subwavesArr)
        {
            foreach(wv_subwave_enemytuple enemyTuple in subwave.enemyTuples)
            {
                amount += enemyTuple.amount;
            }
        }

        return amount;
    }
}
