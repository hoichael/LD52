using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te_sl_wep : te_selectable_base
{
    [SerializeField] wepType type;
    [SerializeField] string ownedText;
    public override void Use()
    {
        base.Use();
    }
}
