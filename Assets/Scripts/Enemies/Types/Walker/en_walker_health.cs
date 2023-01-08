using UnityEngine;

public class en_walker_health : en_health_base
{
    [SerializeField] en_info_base info;

    public override void HandleDamage(dmg_info dmgInfo)
    {
        base.HandleDamage(dmgInfo);
        info.brain.ChangeState("hit");
    }

    protected override void HandleDeath(dmg_info dmgInfo)
    {
        base.HandleDeath(dmgInfo);
        Destroy(info.trans.gameObject);
    }
}
