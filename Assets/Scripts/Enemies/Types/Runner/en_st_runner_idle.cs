using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_runner_idle : en_state_base
{
    [SerializeField] float maxAttackDistance;
    Transform playerTrans;

    private void Start()
    {
        playerTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Idle", 0.1f);
        StartCoroutine(HandleCheckFlag());
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(playerTrans.position, info.trans.position);
        if (distance < maxAttackDistance)
        {
            ChangeState("attack");
        }
        else
        {
            ChangeState("chase");
        }
    }

    private IEnumerator HandleCheckFlag()
    {
        yield return new WaitForSeconds(1f);
    }
}
