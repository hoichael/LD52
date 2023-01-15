using UnityEngine;

public class en_walker_health : en_health_base
{
    [SerializeField] bool dontApplyHitStun;
    [SerializeField] bool isGiant;
    public override void HandleDamage(dmg_info dmgInfo)
    {
        base.HandleDamage(dmgInfo);
        
        if(dontApplyHitStun)
        {
            return;
        }

        info.brain.ChangeState("hit");
    }

    protected override void HandleDeath(dmg_info dmgInfo)
    {
        base.HandleDeath(dmgInfo);

        g_refs.Instance.pool.Dispatch(PoolType.vfx_blood_expl, info.trans.position + Vector3.up * 0.8f, Quaternion.identity);

        //Destroy(info.trans.gameObject);
        if (isGiant)
        {
            g_refs.Instance.pool.Return(PoolType.en_giant, info.trans, false);
        }
        else
        {
            g_refs.Instance.pool.Return(PoolType.en_walker, info.trans, false);
        }
    }
}
