using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_floater_float : en_state_base
{
    [SerializeField] float duration;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSlerpDamp;

    Vector3 floatDir;
    Transform targetTrans;

    private void Start()
    {
        targetTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Float", 0.45f);
        floatDir = Random.insideUnitSphere;
        floatDir = new Vector3(floatDir.x, floatDir.y * 0.5f, floatDir.z);
        StartCoroutine(HandleDuration());
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
        info.rb.AddForce(floatDir * moveSpeed, ForceMode.Acceleration);
    }

    private void LookAtPlayer()
    {
        Vector3 targetVec = targetTrans.position - info.trans.position;
        targetVec.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(targetVec);

        info.trans.localRotation = Quaternion.Slerp(info.trans.localRotation, targetRot, rotSlerpDamp);
    }

    private IEnumerator HandleDuration()
    {
        yield return new WaitForSeconds(duration);
        ChangeState("chase");
    }
}
