using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te_sl_consumable : te_selectable_base
{
    [SerializeField] int hpAmount; // if set to 0 in inspector, its ammo

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
        else
        {
            if(hpAmount != 0)
            {
                if (g_refs.Instance.sessionData.playerHP == g_refs.Instance.sessionData.currentPlMaxHp)
                {
                    Deny();
                }
                else
                {
                    g_refs.Instance.plHealth.HandleHeal(hpAmount);
                    g_refs.Instance.sessionData.cash -= cost;
                    cashTextEl.text = "CASH: " + g_refs.Instance.sessionData.cash;
                }
            }
            else
            {
                foreach(pl_wep_info wepInfo in g_refs.Instance.sessionData.wepInfoDict.Values)
                {
                    if(wepInfo.active && wepInfo.type != wepType.axe)
                    {
                        wepInfo.wepScript.AddAmmo();
                        g_refs.Instance.sessionData.cash -= cost;
                        cashTextEl.text = "CASH: " + g_refs.Instance.sessionData.cash;
                        g_refs.Instance.sfxOneshot2D.Play(SfxType.ammo_get);
                        return;
                    }
                }

                Deny();
            }
        }
    }
}
