using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ui_Manager : MonoBehaviour
{
    [SerializeField] TextMeshPro hTimeText;
    [SerializeField] Vector3 hTimeTextOrigin, hTimeTextTarget;
    [SerializeField] float hTimeTextSpeed;
    [SerializeField] AnimationCurve hTimeAnimCurve;

    [SerializeField] TextMeshPro healthText, moneyText, scoreText;
    [SerializeField] Vector3 healthTextPosTarget, moneyTextPosTarget, scoreTextPosTarget;
    Vector3 healthTextPosStart, moneyTextPosStart, scoreTextPosStart;

    [SerializeField] Transform chicken;
    [SerializeField] Vector3 chickenPosTarget;
    Vector3 chickenPosStart;


    float currentHTimeTextFactor;
    bool harvestTimeAnim;

    public void HandleHarvestTime()
    {
        harvestTimeAnim = true;
        healthTextPosStart = healthText.transform.localPosition;
        moneyTextPosStart = moneyText.transform.localPosition;
        scoreTextPosStart = moneyText.transform.localPosition;
        chickenPosStart = chicken.localPosition;
    }

    private void Update()
    {
        if(harvestTimeAnim)
        {
            HandleHarvestTimeAnim();
        }    
    }

    private void HandleHarvestTimeAnim()
    {
        currentHTimeTextFactor = Mathf.MoveTowards(currentHTimeTextFactor, 1, hTimeTextSpeed * Time.deltaTime);

        hTimeText.transform.localPosition = Vector3.Lerp(
            hTimeTextOrigin,
            hTimeTextTarget,
            hTimeAnimCurve.Evaluate(Mathf.PingPong(currentHTimeTextFactor, 0.5f))
            );

        healthText.transform.localPosition = Vector3.Lerp(
            healthTextPosStart,
            healthTextPosTarget,
            currentHTimeTextFactor
            );

        moneyText.transform.localPosition = Vector3.Lerp(
            moneyTextPosStart,
            moneyTextPosTarget,
            currentHTimeTextFactor
            );

        scoreText.transform.localPosition = Vector3.Lerp(
            scoreTextPosStart,
            scoreTextPosTarget,
            currentHTimeTextFactor
            );

        chicken.localPosition = Vector3.Lerp(
            chickenPosStart,
            chickenPosTarget,
            currentHTimeTextFactor
            );
    }
}
