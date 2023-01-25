using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te_sl_consumable : te_selectable_base
{
    [SerializeField] bool isAmmo; // if it aint ammo..... its health.

    public override void Select()
    {
        base.Select();
    }
}
