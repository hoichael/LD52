using UnityEngine;

public class en_health_shooter : en_health_base
{
    //public override void HandleDamage(dmg_info dmgInfo)
    //{
    //    base.HandleDamage(dmgInfo);
    //    info.brain.ChangeState("hit");
    //}

    protected override void HandleDeath(dmg_info dmgInfo)
    {
        base.HandleDeath(dmgInfo);

        g_refs.Instance.pool.Dispatch(PoolType.vfx_blood_expl, info.trans.position + Vector3.up * 1.85f, Quaternion.identity);
        //g_refs.Instance.pool.Return(PoolType.en_shooter, info.trans, false);
        Destroy(info.trans.gameObject);
    }
}
