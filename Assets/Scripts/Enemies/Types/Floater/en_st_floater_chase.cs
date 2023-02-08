using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_floater_chase : en_state_base
{
    [SerializeField] float moveSpeed;
    [SerializeField] float attackDistance;
    [SerializeField] float rotSlerpDamp;
    Transform playerTrans;

    private void Start()
    {
        playerTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Fly", 0.15f);
    }

    private void FixedUpdate()
    {
        CheckDistance();
        LookAtPlayer();
        MoveTowardsPlayer();
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(playerTrans.position, info.trans.position);
        if (distance < attackDistance)
        {
            ChangeState("attack");
        }
    }

    private void LookAtPlayer()
    {
        Vector3 targetVec = (g_refs.Instance.plTrans.position + Vector3.up * 4f) - info.trans.position;
        Quaternion targetRot = Quaternion.LookRotation(targetVec);

        info.trans.localRotation = Quaternion.Slerp(info.trans.localRotation, targetRot, rotSlerpDamp);
    }

    private void MoveTowardsPlayer()
    {
        info.rb.AddForce(transform.forward * moveSpeed);
    }
}
