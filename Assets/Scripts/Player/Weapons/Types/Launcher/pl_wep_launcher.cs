using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_launcher : pl_wep_base
{
    //[Header("LAUNCHER --- REFS")]


    protected override void Shoot()
    {
        base.Shoot();
        //audioSource.Play();

        //StartCoroutine(HandleMuzzleLight());
        //muzzleFlashParticles.Play();

        refs.generalPool.Dispatch(PoolType.proj_launcher, firePoint.position, Quaternion.Euler(refs.camHolderTrans.rotation.eulerAngles));
        refs.generalPool.Dispatch(PoolType.vfx_launcher_air, firePoint.position, Quaternion.Euler(refs.camHolderTrans.rotation.eulerAngles));
    }
}
