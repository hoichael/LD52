using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_floater_attack : en_state_base
{
    [SerializeField] float shootInterval; // 0.22
    [SerializeField] float durationAfterShooting; // 1 - 0.66
    [SerializeField] float chaseDistance;
    [SerializeField] float rotSlerpDamp;

    [SerializeField] Transform firePointMouth, firePointEye_L, firePointEye_R;
    [SerializeField] AudioSource shootAudioSrc;

    [SerializeField] bool isBoss;

    Transform targetTrans;

    private void Start()
    {
        targetTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Attack", 0.05f);
        StartCoroutine(HandleDelays());
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    private IEnumerator HandleDelays()
    {
        FireProjectile();
        yield return new WaitForSeconds(shootInterval);
        FireProjectile();
        yield return new WaitForSeconds(shootInterval);
        FireProjectile();
        yield return new WaitForSeconds(shootInterval);
        FireProjectile();
        yield return new WaitForSeconds(durationAfterShooting);
        ChangeState("float");
    }

    private void LookAtPlayer()
    {
        Vector3 targetVec = targetTrans.position - info.trans.position;
        targetVec.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(targetVec);

        info.trans.localRotation = Quaternion.Slerp(info.trans.localRotation, targetRot, rotSlerpDamp);
    }

    private void FireProjectile()
    {
        if(isBoss)
        {
            firePointEye_L.LookAt(g_refs.Instance.plTrans.position);
            firePointEye_R.LookAt(g_refs.Instance.plTrans.position);
            g_refs.Instance.pool.Dispatch(PoolType.proj_en_floater_boss, firePointEye_L.position, firePointEye_L.rotation);
            g_refs.Instance.pool.Dispatch(PoolType.proj_en_floater_boss, firePointEye_R.position, firePointEye_R.rotation);
        }
        else
        {
            firePointMouth.LookAt(g_refs.Instance.plTrans.position);
            g_refs.Instance.pool.Dispatch(PoolType.proj_en_shooter, firePointMouth.position, firePointMouth.rotation);
        }
        g_refs.Instance.pool.Dispatch(PoolType.vfx_mflash_en_shooter, firePointMouth.position, firePointMouth.rotation);
        shootAudioSrc.Play();
    }
}
