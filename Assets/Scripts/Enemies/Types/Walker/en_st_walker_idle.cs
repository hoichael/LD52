using System.Collections;
using UnityEngine;

public class en_st_walker_idle : en_state_base
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
        yield return new WaitForSeconds(0.1f);
        conductChecks = true;
    }

    protected override void OnDisable()
    {
        base.OnEnable();
    }
}
