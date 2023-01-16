using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_shooter_idle : en_state_base
{
    [SerializeField] float maxChaseDistance, maxAttackDistance;
    Transform playerTrans;

    bool conductChecks;
    private void Start()
    {
        playerTrans = g_refs.Instance.plTrans;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        info.rb.velocity = Vector3.zero;
        info.anim.CrossFade("Idle", 0.3f);
        StartCoroutine(HandleCheckFlag());
    }

    private void FixedUpdate()
    {
        if (!conductChecks) return;

        float distance = Vector3.Distance(playerTrans.position, info.trans.position);
        if (distance < maxAttackDistance)
        {
            ChangeState("attack");
        }
        //else if(distance < maxChaseDistance)
        else
        {
            ChangeState("chase");
        }
    }

    private IEnumerator HandleCheckFlag()
    {
        conductChecks = false;
        yield return new WaitForSeconds(0.35f);
        conductChecks = true;
    }
}
