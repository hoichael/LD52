using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// LEGACY, UNUSED
public class ui_Manager : MonoBehaviour
{
    public TextMeshPro hTimeText;
    public Vector3 hTimeTextOrigin, hTimeTextTarget;
    public float hTimeTextSpeed;
    public AnimationCurve hTimeAnimCurve;

    public TextMeshPro healthText, moneyText, scoreText;
    public Vector3 healthTextPosTarget, moneyTextPosTarget, scoreTextPosTarget;
    public Vector3 healthTextPosStart, moneyTextPosStart, scoreTextPosStart;

    public Transform chicken;
    public Vector3 chickenPosTarget;
    public Vector3 chickenPosStart;


    public TextMeshPro waveCounterText, enemyCounterText;
    public Vector3 waveCounterPosTarget, enemyCounterPosTarget;
    public Vector3 waveCounterPosStart, enemyCounterPosStart;

    float currentHTimeTextFactor;
    bool harvestTimeAnim;

    [SerializeField] ui_setup_init uiHandlerInit;
    [SerializeField] ui_setup_consecutive uiHanderConsecutive;

    public Transform uiBar;
    public Vector3 uiBarPosStart, uiBarPosTarget;

    private void Start()
    {
        if(g_refs.Instance.sessionData.currentWaveRegular > 0)
        {
            uiHanderConsecutive.HandleSceneLoad();
        }
    }

    public void HandleHarvestTime()
    {
        //harvestTimeAnim = true;
        healthTextPosStart = healthText.transform.localPosition;
        moneyTextPosStart = moneyText.transform.localPosition;
        scoreTextPosStart = scoreText.transform.localPosition;
        //chickenPosStart = chicken.localPosition;
        waveCounterPosStart = waveCounterText.transform.localPosition;
        enemyCounterPosStart = enemyCounterText.transform.localPosition;

        enemyCounterText.gameObject.SetActive(true);

        if (g_refs.Instance.sessionData.currentWaveRegular > 0)
        {
            uiHanderConsecutive.HandleHarvestTime();
        }
        else
        {
            uiHandlerInit.HandleHarvestTime();
        }
    }

    //private void Update()
    //{
    //    if(harvestTimeAnim)
    //    {
    //        HandleHarvestTimeAnim();
    //    }    
    //}

    //private void HandleHarvestTimeAnim()
    //{
    //    currentHTimeTextFactor = Mathf.MoveTowards(currentHTimeTextFactor, 1, hTimeTextSpeed * Time.deltaTime);

    //    hTimeText.transform.localPosition = Vector3.Lerp(
    //        hTimeTextOrigin,
    //        hTimeTextTarget,
    //        hTimeAnimCurve.Evaluate(Mathf.PingPong(currentHTimeTextFactor, 0.5f))
    //        );

    //    healthText.transform.localPosition = Vector3.Lerp(
    //        healthTextPosStart,
    //        healthTextPosTarget,
    //        currentHTimeTextFactor
    //        );

    //    moneyText.transform.localPosition = Vector3.Lerp(
    //        moneyTextPosStart,
    //        moneyTextPosTarget,
    //        currentHTimeTextFactor
    //        );

    //    scoreText.transform.localPosition = Vector3.Lerp(
    //        scoreTextPosStart,
    //        scoreTextPosTarget,
    //        currentHTimeTextFactor
    //        );

    //    chicken.localPosition = Vector3.Lerp(
    //        chickenPosStart,
    //        chickenPosTarget,
    //        currentHTimeTextFactor
    //        );

    //    waveCounterText.transform.localPosition = Vector3.Lerp(
    //        waveCounterPosStart,
    //        waveCounterPosTarget,
    //        currentHTimeTextFactor
    //        );

    //    enemyCounterText.transform.localPosition = Vector3.Lerp(
    //        enemyCounterPosStart,
    //        enemyCounterPosTarget,
    //        currentHTimeTextFactor
    //        );

    //    if (currentHTimeTextFactor == 1) harvestTimeAnim = false;
    //}
}
