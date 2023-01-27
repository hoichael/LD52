using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_exploder_chase : en_state_base
{
    [SerializeField] float rotSlerpDamp;
    [SerializeField] float moveSpeed;
    [SerializeField] float attackDistance;
    [SerializeField] AudioSource runAudioSrc;
    Transform playerTrans;

    private void Start()
    {
        playerTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        info.anim.CrossFade("Run", 0.3f);
        runAudioSrc.Play();
    }

    private void FixedUpdate()
    {
        CheckDistance();
        LookAtPlayer();
        info.rb.AddForce(transform.forward * moveSpeed);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(playerTrans.position, info.trans.position);
        if (distance < attackDistance)
        {
            ChangeState("Charge");
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
        base.OnEnable();
    }
}
