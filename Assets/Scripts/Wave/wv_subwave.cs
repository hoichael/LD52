using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wv_subwave_", menuName = "ScriptableObjects/Wave/Subwave", order = 1)]
public class wv_subwave : ScriptableObject
{
    public Vector3[] spawnPoints;
    public wv_subwave_enemytuple[] enemyTuples;
}

[System.Serializable]
public class wv_subwave_enemytuple
{
    public PoolType type;
    public int amount;
}
