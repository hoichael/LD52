using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te_sl_consumable : te_selectable_base
{
    //[SerializeField] bool isAmmo; // if it aint ammo..... its health.
    [SerializeField] int hpAmount;

    public override void Select()
    {
        base.Select();
    }

    public override void Use()
    {
        base.Use();
        if (g_refs.Instance.sessionData.cash < cost)
        {
            Deny();
        }
        else if (g_refs.Instance.sessionData.playerHP == g_refs.Instance.sessionData.currentPlMaxHp)
        {
            Deny();
        }
        else
        {
            g_refs.Instance.plHealth.HandleHeal(hpAmount);
            g_refs.Instance.sessionData.cash -= cost;
        }
    }
}
