using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_exploder_chase : en_state_base
{
    [SerializeField] float rotSlerpDamp;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxVelMagnitude;
    [SerializeField] float attackDistance;
    [SerializeField] AudioSource runAudioSrc;
    [SerializeField] en_forces forcesHandler;
    Transform playerTrans;

    private void Start()
    {
        playerTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        info.anim.CrossFade("Run", 0.3f);
        forcesHandler.moveForward = true;
    }

    private void FixedUpdate()
    {
        CheckDistance();
        LookAtPlayer();
        HandleRunAudio();
    }

    private void HandleRunAudio()
    {
        if (!info.grounded || this.enabled == false /* unity lifecycling be trippin */)
        {
            runAudioSrc.Stop();
        }
        else
        {
            if (!runAudioSrc.isPlaying)
            {
                runAudioSrc.Play();
            }
        }
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
        base.OnDisable();
        forcesHandler.moveForward = false;
        runAudioSrc.Stop();
    }
}
