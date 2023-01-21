using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wv_subwave_", menuName = "ScriptableObjects/Wave/Subwave", order = 1)]
public class wv_subwave : ScriptableObject
{
    public Vector3[] spawnPointsArr;
    public wv_subwave_enemyinfo[] enemyInfoArr;
    public float spawnDelay;
}

[System.Serializable]
public class wv_subwave_enemyinfo
{
    public PoolType type;
    public int amount;
}
