using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wv_wave : MonoBehaviour
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
            foreach(wv_subwave_enemyinfo enemyInfo in subwave.enemyInfoArr)
            {
                amount += enemyInfo.amount;
            }
        }

        return amount;
    }
}
