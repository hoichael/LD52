using System.Collections;
using UnityEngine;

public class pl_wep_shotgun : pl_wep_base
{
    [Header("SHOTGUN --- REFS")]
    [SerializeField] ParticleSystem muzzleFlashParticles;
    [SerializeField] Light muzzleFlashLight;

    [Header("SHOTGUN --- VALUES")]
    [SerializeField] int bulletAmount;
    [SerializeField] float bulletSpreadRangeHalf;
    [SerializeField] float muzzleFlashDuration;

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

        if (Physics.Raycast(refs.camHolderTrans.position, bulletDir, out hit, 100, refs.enemyLayerMask))
        {
            //print("HIT ENEMY with weapon of ID '" + ID + "'");

            hit.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
            refs.generalPool.Dispatch(PoolType.vfx_blood, hit.point, Quaternion.identity);
            refs.tracerPool.Dispatch(firePoint.position, hit.point, pl_wep_tracertype.Rifle);
        }
        else
        {
            refs.tracerPool.Dispatch(firePoint.position, refs.camHolderTrans.position + bulletDir * 65, pl_wep_tracertype.Shotgun);
        }
    }

    private Vector3 GetRandomBulletDir()
    {
        Vector3 offset = new Vector3(
            Random.Range(-bulletSpreadRangeHalf, bulletSpreadRangeHalf),
            Random.Range(-bulletSpreadRangeHalf, bulletSpreadRangeHalf),
            Random.Range(-bulletSpreadRangeHalf, bulletSpreadRangeHalf)
            );

        return refs.camHolderTrans.forward + offset;
    }

    protected override void ResetVisuals()
    {
        base.ResetVisuals();
        muzzleFlashLight.enabled = false;
    }

    private IEnumerator HandleMuzzleLight()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlashLight.enabled = false;
    }
}
