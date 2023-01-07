using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nv_int_D_dispenser : nv_int_base
{
    [SerializeField] int hpAmount;
    public override void HandleInteract()
    {
        g_refs.Instance.plHealth.HandleHeal(hpAmount);
    }
}
