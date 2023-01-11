using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_rifle : pl_wep_base
{
    [SerializeField] GameObject bloodVFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] lv_pool pool;

    [SerializeField] Light muzzleFlashLight;
    [SerializeField] float muzzleFlashDuration;

    [SerializeField] Transform firePoint;

    private void Start()
    {
        muzzleFlashLight.enabled = false;
    }

    protected override void Shoot()
    {
        base.Shoot();

        audioSource.Play();
        StartCoroutine(HandleMuzzleLight());

        pool.Dispatch(PoolType.vfx_muzzleflash, firePoint.position);

        RaycastHit hit;
        if (Physics.Raycast(camHolderTrans.position, camHolderTrans.forward, out hit, 100, enemyLayerMask))
        {
            print("HIT ENEMY with weapon of ID '" + ID + "'");
            hit.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
            //Instantiate(bloodVFX, hit.point, Quaternion.identity);
            pool.Dispatch(PoolType.vfx_blood, hit.point);
        }
    }

    private IEnumerator HandleMuzzleLight()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlashLight.enabled = false;
    }
}
