using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_floater_health : en_health_base
{
    public override void HandleDamage(dmg_info dmgInfo)
    {
        base.HandleDamage(dmgInfo);

        //if (hpCurrent > 0)
        //{
        //    info.brain.ChangeState("hit");
        //}
    }

    protected override void HandleDeath(dmg_info dmgInfo)
    {
        base.HandleDeath(dmgInfo);

        g_refs.Instance.pool.Dispatch(PoolType.vfx_blood_expl, info.trans.position + Vector3.up * 1.85f, Quaternion.identity);
        Destroy(info.trans.gameObject);
    }
}
