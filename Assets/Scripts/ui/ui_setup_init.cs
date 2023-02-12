using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_setup_init : MonoBehaviour
{
    [SerializeField] ui_Manager manager;

    float currentTransitionFactor = 1;

    public void HandleHarvestTime()
    {
        currentTransitionFactor = 0;
    }

    private void Update()
    {
        if (currentTransitionFactor != 1) HandleHarvestTimeAnim();
    }

    private void HandleHarvestTimeAnim()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, 1, manager.hTimeTextSpeed * Time.deltaTime);

        manager.hTimeText.transform.localPosition = Vector3.Lerp(
            manager.hTimeTextOrigin,
            manager.hTimeTextTarget,
            manager.hTimeAnimCurve.Evaluate(Mathf.PingPong(currentTransitionFactor, 0.5f))
            );

        manager.uiBar.localPosition = Vector3.Lerp(
            manager.uiBarPosStart,
            manager.uiBarPosTarget,
            currentTransitionFactor
            );

        manager.healthText.transform.localPosition = Vector3.Lerp(
            manager.healthTextPosStart,
            manager.healthTextPosTarget,
            currentTransitionFactor
            );

        manager.moneyText.transform.localPosition = Vector3.Lerp(
            manager.moneyTextPosStart,
            manager.moneyTextPosTarget,
            currentTransitionFactor
            );

        manager.scoreText.transform.localPosition = Vector3.Lerp(
            manager.scoreTextPosStart,
            manager.scoreTextPosTarget,
            currentTransitionFactor
            );

        //manager.chicken.localPosition = Vector3.Lerp(
        //    manager.chickenPosStart,
        //    manager.chickenPosTarget,
        //    currentTransitionFactor
        //    );

        manager.waveCounterText.transform.localPosition = Vector3.Lerp(
            manager.waveCounterPosStart,
            manager.waveCounterPosTarget,
            currentTransitionFactor
            );

        manager.enemyCounterText.transform.localPosition = Vector3.Lerp(
            manager.enemyCounterPosStart,
            manager.enemyCounterPosTarget,
            currentTransitionFactor
            );
    }
}
