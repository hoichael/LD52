using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_sway : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [Space]

    [Header("Position")]
    [SerializeField] float posSwayStrengthBase;
    [SerializeField] float posSwayClamp;
    [SerializeField] float posSmoothFactor;
    Vector3 currentDefaultPos;
    float currentPosSwayStrength;

    [Header("Rotation")]
    [SerializeField] float rotSwayStrengthBase;
    [SerializeField] float rotSwayClamp;
    [SerializeField] float rotSmoothFactor;
    Quaternion defaultRotation;
    float currentRotSwayStrength;

    Transform currentSwayTrans;

    public bool rotateOnX, rotateOnY, rotateOnZ;

    float mouseInputX;
    float mouseInputY;

    private void Awake()
    {
        defaultRotation = Quaternion.Euler(Vector3.zero);
        currentDefaultPos = Vector3.zero;
    }

    public void OnWeaponSwitch(Vector3 wepDefaultPos, Transform newSwayPivotContainer, float weightMult)
    {
        currentSwayTrans = newSwayPivotContainer;
        currentDefaultPos = wepDefaultPos;
        currentPosSwayStrength = posSwayStrengthBase * weightMult;
        currentRotSwayStrength = rotSwayStrengthBase * weightMult;
    }

    private void Update()
    {
        if (currentSwayTrans == null) return;

        GetInput();

        ExecutePosSway();
        ExecuteTiltSway();
    }

    private void ExecutePosSway()
    {
        float processedX = Mathf.Clamp(mouseInputX * currentPosSwayStrength, -posSwayClamp, posSwayClamp);
        float processedY = Mathf.Clamp(mouseInputY * currentPosSwayStrength, -posSwayClamp, posSwayClamp);

        Vector3 targetPos = new Vector3(processedX, processedY, 0);

        currentSwayTrans.localPosition = Vector3.Lerp(currentSwayTrans.localPosition, targetPos + currentDefaultPos, Time.deltaTime * posSmoothFactor);
    }

    private void ExecuteTiltSway()
    {
        float processedX = Mathf.Clamp(mouseInputY * currentRotSwayStrength, -rotSwayClamp, rotSwayClamp);
        float processedY = Mathf.Clamp(mouseInputX * currentRotSwayStrength, -rotSwayClamp, rotSwayClamp);

        Quaternion targetRot = Quaternion.Euler(new Vector3(rotateOnX ? -processedX : 0f, rotateOnY ? processedY : 0f, rotateOnZ ? processedY : 0f));

        currentSwayTrans.localRotation = Quaternion.Slerp(currentSwayTrans.localRotation, targetRot * defaultRotation, Time.deltaTime * rotSmoothFactor);
    }

    private void GetInput()
    {
        mouseInputX = -(Input.GetAxis("Mouse X") * refs.settings.mouseSens);
        mouseInputY = -(Input.GetAxis("Mouse Y") * refs.settings.mouseSens);
    }
}
