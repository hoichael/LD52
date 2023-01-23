using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_exploder_explode : en_state_base
{
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0);
        g_refs.Instance.pool.Return(PoolType.en_exploder, info.trans, false);
        g_refs.Instance.pool.Dispatch(PoolType.en_exploder_explosion, info.trans.position, Quaternion.identity);
    }
}
