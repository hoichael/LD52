using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_shooter_attack : en_state_base
{
    [SerializeField] float timeForFirstShot, timeForSecondShot, timeForExit;

    [SerializeField] float rotSlerpDamp;

    Transform targetTrans;

    [SerializeField] Transform firePointTrans;

    private void Start()
    {
        targetTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        info.rb.velocity = Vector3.zero;
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
        yield return new WaitForSeconds(timeForFirstShot);
        Shoot();

        //yield return new WaitForSeconds(timeForSecondShot);
        //Shoot();

        yield return new WaitForSeconds(timeForExit);

        if(Random.Range(0, 1f) < 0.7f)
        {
            ChangeState("idle");
        }
        else
        {
            ChangeState("jump");
        }
    }

    private void Shoot()
    {
        firePointTrans.LookAt(g_refs.Instance.plTrans.position);
        g_refs.Instance.pool.Dispatch(PoolType.proj_en_shooter, firePointTrans.position, firePointTrans.rotation);
    }
    

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }
}
