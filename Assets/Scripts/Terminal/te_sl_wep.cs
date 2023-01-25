using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te_sl_wep : te_selectable_base
{
    [SerializeField] wepType type;
    [SerializeField] pl_wep_manager wepManager;

    public override void Select()
    {
        base.Select();
        if (g_refs.Instance.sessionData.wepInfoDict[type].owned)
        {
            terminalManager.textElLower.text = "OWNED";
        }
    }

    public override void Use()
    {
        base.Use();
        if (g_refs.Instance.sessionData.cash < cost)
        {
            Deny();
        }
        else if (g_refs.Instance.sessionData.wepInfoDict[type].owned)
        {
            Deny();
        }
        else
        {
            g_refs.Instance.sessionData.cash -= cost;
            wepManager.SwitchWeapon(type);
            terminalManager.textElLower.text = "OWNED";
        }
    }
}
