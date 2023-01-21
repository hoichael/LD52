using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wv_wave_", menuName = "ScriptableObjects/Wave/Wave", order = 0)]
public class wv_wave : ScriptableObject
{
    public wv_subwave[] subwavesArr;

    public int AmountOfEnemies()
    {
        return GetTotalEnemyAmount();
    }

    private int GetTotalEnemyAmount()
    {
        int amount = 0;

        foreach(wv_subwave subwave in subwavesArr)
        {
            foreach(wv_subwave_enemyinfo enemyTuple in subwave.enemyInfoArr)
            {
                amount += enemyTuple.amount;
            }
        }

        return amount;
    }
}
