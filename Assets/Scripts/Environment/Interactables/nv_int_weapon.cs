using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nv_int_weapon : nv_int_base
{
    public override void HandleInteract()
    {
        base.HandleInteract();
        Destroy(gameObject);
    }
}
