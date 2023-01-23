using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_exploder_charge : en_state_base
{
    [SerializeField] float duration;

    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Explode", 0.2f);
        StartCoroutine(HandleDuration());
    }

    private IEnumerator HandleDuration()
    {
        yield return new WaitForSeconds(duration);
        ChangeState("Explode");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }
}
