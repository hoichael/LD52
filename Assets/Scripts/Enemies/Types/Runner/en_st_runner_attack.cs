using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_runner_attack : en_state_base
{
    [SerializeField] float duration;

    [SerializeField] float hitboxDelay;

    [SerializeField] float rotSlerpDamp;

    Transform targetTrans;

    [SerializeField] dmg_info dmgInfo;

    [SerializeField] Transform attackPosTrans;

    [SerializeField] float attackRadius;

    [SerializeField] LayerMask playerLayerMask;


    private void Start()
    {
        targetTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        info.anim.CrossFade("Attack", 0.3f);

        StartCoroutine(HandleDuration());
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
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
        yield return new WaitForSeconds(hitboxDelay);
        HandleHitbox();

        yield return new WaitForSeconds(duration - hitboxDelay);
        ChangeState("idle");
    }

    private void HandleHitbox()
    {
        if (Physics.CheckSphere(
            attackPosTrans.position,
            attackRadius,
            playerLayerMask))
        {
            g_refs.Instance.plHealth.HandleDamage(dmgInfo);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }
}
