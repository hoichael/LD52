using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nv_int_terminal : nv_int_base
{
    [SerializeField] te_manager terminalManager;
    [SerializeField, Range(-1, 1)] int idInt; // -1 for left, 1 for right, 0 for confirm button

    public override void HandleInteract()
    {
        base.HandleInteract();
        terminalManager.HandleButtonPress(idInt);
    }
}
