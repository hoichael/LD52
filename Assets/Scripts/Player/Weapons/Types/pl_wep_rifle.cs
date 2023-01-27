using System.Collections;
using UnityEngine;

public class pl_wep_rifle : pl_wep_base
{
    [Header("RIFLE --- REFS")]
    [SerializeField] Light muzzleFlashLight;
    [SerializeField] ParticleSystem muzzleFlashParticles;
    
    [Header("RIFLE --- VALUES")]
    [SerializeField] float muzzleFlashDuration;

    private void Start()
    {
        muzzleFlashLight.enabled = false;
    }

    protected override void Shoot()
    {
        base.Shoot();

        //audioSource.Play();
        muzzleFlashParticles.Play();
        StartCoroutine(HandleMuzzleLight());

        RaycastHit hit;
        if (Physics.Raycast(refs.camHolderTrans.position, refs.camHolderTrans.forward, out hit, 200, refs.enemyLayerMask))
        {
            //print("HIT ENEMY with weapon of ID '" + ID + "'");

            hit.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
            refs.generalPool.Dispatch(PoolType.vfx_blood, hit.point, Quaternion.identity);
            refs.tracerPool.Dispatch(firePoint.position, hit.point, pl_wep_tracertype.Rifle);
        }
        else
        {
            refs.tracerPool.Dispatch(firePoint.position, refs.camHolderTrans.position + refs.camHolderTrans.forward * 65, pl_wep_tracertype.Rifle);
        }
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
