using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_runner_health : en_health_base
{
    protected override void HandleDeath(dmg_info dmgInfo)
    {
        base.HandleDeath(dmgInfo);
        g_refs.Instance.pool.Dispatch(PoolType.vfx_blood_expl, info.trans.position + Vector3.up * 0.8f, Quaternion.identity);
        Destroy(info.trans.gameObject);
    }
}
