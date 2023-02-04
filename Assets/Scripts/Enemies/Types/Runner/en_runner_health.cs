using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_runner_health : en_health_base
{
    protected override void HandleDeath(dmg_info dmgInfo)
    {
        base.HandleDeath(dmgInfo);

        Destroy(info.trans.gameObject);
    }
}
