using System.Collections;
using UnityEngine;

public class en_st_walker_hit : en_state_base
{
    [SerializeField] float hitDuration;
    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Hit", 0.1f);

        StartCoroutine(HandleDuration());
    }

    private IEnumerator HandleDuration()
    {
        yield return new WaitForSeconds(hitDuration);
        ChangeState("idle");
    }
}
