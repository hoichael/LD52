using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_runner_chase : en_state_base
{
    [SerializeField] float rotSlerpDamp;
    [SerializeField] float attackDistance;
    [SerializeField] en_forces forcesHandler;
    [SerializeField] AudioSource runAudioSrc;
    Transform playerTrans;

    private void Start()
    {
        playerTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        info.anim.CrossFade("Run", 0.25f);
        forcesHandler.moveForward = true;

        StartCoroutine(HandleRunDustVFX());
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

    private IEnumerator HandleRunDustVFX()
    {
        if (info.grounded)
        {
            g_refs.Instance.pool.Dispatch(PoolType.vfx_dust_run, info.groundcheckPos.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(HandleRunDustVFX());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        forcesHandler.moveForward = false;
        runAudioSrc.Stop();
        StopAllCoroutines();
    }
}
