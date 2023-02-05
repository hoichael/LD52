using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_floater_hit : en_state_base
{
    [SerializeField] float duration;

    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Hit", 0.03f);
        StartCoroutine(HandleDuration());
    }

    private IEnumerator HandleDuration()
    {
        yield return new WaitForSeconds(duration);
        ChangeState("chase");
    }
}
