using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wep_DEV : pl_wep_base
{
    protected override void Shoot()
    {
        base.Shoot();
        RaycastHit hit;
        if (Physics.Raycast(camHolderTrans.position, camHolderTrans.forward, out hit, 100, enemyLayerMask))
        {
            print("HIT ENEMY with weapon of ID '" + ID + "'");
        }
    }
}
