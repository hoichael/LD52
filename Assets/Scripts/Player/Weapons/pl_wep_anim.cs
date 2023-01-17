using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_anim : MonoBehaviour
{
    [Header("GENERAL")]
    [SerializeField] pl_refs refs;

    [Header("BOB")]
    [SerializeField] Transform bobContainer;
    [SerializeField, Range(0, 0.1f)] float baseAmplitude;
    [SerializeField, Range(0, 30f)] float baseFrequency;
    [SerializeField] float bobPlMagThreshold;
    [SerializeField] float bobResetSpeed;
    float currentBobAmplitude;

    [Header("SWAY")]
    [SerializeField] Transform swayContainer;

    public void OnWeaponSwitch(float newWepBobAmpFactor)
    {
        bobContainer.transform.localPosition = Vector3.zero;
        currentBobAmplitude = baseAmplitude * newWepBobAmpFactor;
    }

    private void Update()
    {
        HandleBob();
    }

    private void HandleBob()
    {
        if(refs.state.grounded && new Vector3(refs.playerRB.velocity.x, 0, refs.playerRB.velocity.z).magnitude > bobPlMagThreshold)
        {
            ExecuteBob(GetNewBobPos());
        }
        else
        {
            ResetBob();
        }
    }

    private Vector3 GetNewBobPos()
    {
        Vector3 newPos = Vector3.zero;
        newPos.y += Mathf.Sin(Time.time * baseFrequency) * currentBobAmplitude;
        newPos.x += Mathf.Cos(Time.time * baseFrequency / 2) * currentBobAmplitude * 2;
        return newPos;
    }

    private void ExecuteBob(Vector3 newBobPos)
    {
        bobContainer.localPosition += newBobPos;
    }

    private void ResetBob()
    {
        bobContainer.localPosition = Vector3.MoveTowards(bobContainer.localPosition, Vector3.zero, bobResetSpeed * Time.deltaTime);
    }
}
