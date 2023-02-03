using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wv_subwave : MonoBehaviour
{
    //public Vector3[] spawnPointsArr;
    public Transform spawnPointsContainer;
    public wv_subwave_enemyinfo[] enemyInfoArr;
    public float spawnDelay;
}

[System.Serializable]
public class wv_subwave_enemyinfo
{
    public en_type type;
    public int amount;
}
