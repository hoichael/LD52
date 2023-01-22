using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pd_session_RESET : MonoBehaviour
{
    [SerializeField] pd_session sessionData;
    [SerializeField] bool reset;

    private void OnDisable()
    {
        if (!reset) return;
        ResetToDefaults(); // DEV STUFF, ONLY MATTERS IN EDITOR, COMMENT OUT FOR BUILDS
    }

    private void Awake()
    {
        if (!reset) return;
        ResetToDefaults(); // DEV STUFF, ONLY MATTERS IN EDITOR, COMMENT OUT FOR BUILDS
    }

    private void ResetToDefaults() // DEV STUFF, ONLY MATTERS IN EDITOR, COMMENT OUT FOR BUILDS
    {
        sessionData.score = 0;
        sessionData.playerHP = 0;
        sessionData.currentWaveTotal = 1;
        sessionData.currentWaveRegular = 0;
        sessionData.currentWaveLooping = -1;
        sessionData.upgradeLevelHealth = 0;
        sessionData.upgradeLevelMove = 0;
        sessionData.upgradeLevelJump = 0;
        sessionData.wepInfoDict = null;
    }
}
