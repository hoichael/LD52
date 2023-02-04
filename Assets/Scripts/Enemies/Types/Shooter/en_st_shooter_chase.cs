using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_shooter_chase : en_state_base
{
    [SerializeField] float rotSlerpDamp;
    [SerializeField] float moveSpeed;
    [SerializeField] float attackDistance;
    [SerializeField] float maxVelMagnitude;
    [SerializeField] en_forces forcesHandler;
    Transform playerTrans;

    private void Start()
    {
        playerTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        info.anim.CrossFade("Walk", 0.3f);
        forcesHandler.moveForward = true;
    }

    private void FixedUpdate()
    {
        CheckDistance();
        LookAtPlayer();
        info.rb.AddForce(transform.forward * moveSpeed);
        info.rb.velocity = Vector3.ClampMagnitude(info.rb.velocity, maxVelMagnitude);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(playerTrans.position, info.trans.position);
        if (distance < attackDistance)
        {
            //ChangeState("attack");
            ChangeState("idle"); // idle will instantly change to attack state -> having single entry point is cleaner (especially in the animator gui mess)
        }
    }

    private void LookAtPlayer()
    {
        Vector3 targetVec = g_refs.Instance.plTrans.position - info.trans.position;
        targetVec.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(targetVec);

        info.trans.localRotation = Quaternion.Slerp(info.trans.localRotation, targetRot, rotSlerpDamp);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        forcesHandler.moveForward = false;
    }
}
