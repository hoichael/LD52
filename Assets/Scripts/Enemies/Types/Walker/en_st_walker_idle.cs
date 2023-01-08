using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_walker_idle : en_state_base
{
    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.SetBool("idle", true);
    }
}
