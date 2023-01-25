using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te_sl_upgrade : te_selectable_base
{
    [SerializeField] string upgradeType;
    [SerializeField] pl_upgrade_manager upgradeManager;
    int currentCost;

    public override void Select()
    {
        base.Select();
        UpdateCost();
    }

    public override void Use()
    {
        base.Use();
        if (g_refs.Instance.sessionData.cash < currentCost)
        {
            Deny();
        }
        else if (currentCost == 0)
        {
            Deny();
        }
        else
        {
            upgradeManager.TryUpgrade(upgradeType);
            UpdateCost();
        }
    }

    private void UpdateCost()
    {
        currentCost = upgradeManager.GetCurrentCost(upgradeType);
        if (currentCost == 0)
        {
            terminalManager.textElLower.text = "MAXED OUT";
        }
        else
        {
            terminalManager.textElLower.text = "Cost: " + currentCost;
        }
    }
}
