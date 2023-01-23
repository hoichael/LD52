using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_health_exploder : en_health_base
{
    protected override void HandleDeath(dmg_info dmgInfo)
    {
        base.HandleDeath(dmgInfo);
        info.brain.ChangeState("Explode");
    }
}
