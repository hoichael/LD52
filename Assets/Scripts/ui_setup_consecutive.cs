using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_setup_consecutive : MonoBehaviour
{
    [SerializeField] ui_Manager manager;
    float currentTransitionFactor = 1;

    public void HandleSceneLoad()
    {
        manager.uiBar.localPosition = manager.uiBarPosTarget;
        manager.healthText.transform.localPosition = manager.healthTextPosTarget;
        //manager.moneyText.transform.position = manager.moneyTextPosTarget;
        manager.scoreText.transform.localPosition = manager.scoreTextPosTarget;
        manager.moneyText.transform.localPosition = new Vector3(-25.85f, 7.42f, 0);
    }

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

        manager.chicken.localPosition = Vector3.Lerp(
            manager.chickenPosStart,
            manager.chickenPosTarget,
            currentTransitionFactor
            );

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
