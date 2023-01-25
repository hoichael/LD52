using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionData", menuName = "ScriptableObjects/Persistent Data", order = 0)]
public class pd_session : ScriptableObject
{
    public int score;
    public int cash;
    public int playerHP;
    public int currentPlMaxHp;

    public int currentWaveTotal = 1;
    public int currentWaveRegular = 0;
    public int currentWaveLooping = -1;

    public int upgradeLevelHealth;
    public int upgradeLevelMove;
    public int upgradeLevelJump;

    public Dictionary<wepType, pl_wep_info> wepInfoDict;
}
