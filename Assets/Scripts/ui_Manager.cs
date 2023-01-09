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

    float currentHTimeTextFactor;
    bool harvestTimeAnim;

    public void HandleHarvestTime()
    {
        harvestTimeAnim = true;
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
    }
}
