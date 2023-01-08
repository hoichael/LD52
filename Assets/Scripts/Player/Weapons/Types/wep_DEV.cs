using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wep_DEV : pl_wep_base
{
    [SerializeField] int dmgAmount;
    [SerializeField] dmg_info dmgInfo;
    [SerializeField] GameObject bloodVFX;
    protected override void Shoot()
    {
        base.Shoot();
        RaycastHit hit;
        if (Physics.Raycast(camHolderTrans.position, camHolderTrans.forward, out hit, 100, enemyLayerMask))
        {
            print("HIT ENEMY with weapon of ID '" + ID + "'");
            hit.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
            Instantiate(bloodVFX, hit.point, Quaternion.identity);
        }
    }
}
