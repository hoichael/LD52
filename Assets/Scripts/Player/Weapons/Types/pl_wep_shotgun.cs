using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_shotgun : pl_wep_base
{
    [SerializeField] GameObject bloodVFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] lv_pool pool;

    [SerializeField] int bulletAmount;
    [SerializeField] float bulletSpreadRangeHalf;

    [SerializeField] Light muzzleFlashLight;
    [SerializeField] float muzzleFlashDuration;

    [SerializeField] ParticleSystem muzzleFlashParticles;

    [SerializeField] Transform firePoint;
    [SerializeField] pl_wep_tracers_pool tracersPool;

    private void Start()
    {
        muzzleFlashLight.enabled = false;
    }

    protected override void Shoot()
    {
        base.Shoot();
        audioSource.Play();
        StartCoroutine(HandleMuzzleLight());

        muzzleFlashParticles.Play();

        for (int i = 0; i < bulletAmount; i++)
        {
            HandleRay();
        }

    }

    private void HandleRay()
    {
        RaycastHit hit;

        Vector3 bulletDir = GetRandomBulletDir();

        if (Physics.Raycast(camHolderTrans.position, bulletDir, out hit, 100, enemyLayerMask))
        {
            //print("HIT ENEMY with weapon of ID '" + ID + "'");

            hit.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
            pool.Dispatch(PoolType.vfx_blood, hit.point);
            tracersPool.Dispatch(firePoint.position, hit.point, pl_wep_tracertype.Rifle);
        }
        else
        {
            tracersPool.Dispatch(firePoint.position, camHolderTrans.position + bulletDir * 65, pl_wep_tracertype.Shotgun);
        }
    }

    private Vector3 GetRandomBulletDir()
    {
        Vector3 offset = new Vector3(
            Random.Range(-bulletSpreadRangeHalf, bulletSpreadRangeHalf),
            Random.Range(-bulletSpreadRangeHalf, bulletSpreadRangeHalf),
            Random.Range(-bulletSpreadRangeHalf, bulletSpreadRangeHalf)
            );

        return camHolderTrans.forward + offset;
    }

    private IEnumerator HandleMuzzleLight()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlashLight.enabled = false;
    }
}
