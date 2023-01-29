using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_axe : pl_wep_base
{
    [Header("AXE --- VALUES")]
    [SerializeField] float meleeRange;
    [SerializeField] Vector3 swingAnimPosTarget;
    [SerializeField] Vector3 swingAnimRotTarget;

    protected override void Shoot()
    {
        base.Shoot();
        //audioSource.Play();
        //muzzleFlashParticles.Play();
        //StartCoroutine(HandleMuzzleLight());

        RaycastHit hit;
        if (Physics.Raycast(refs.camHolderTrans.position, refs.camHolderTrans.forward, out hit, meleeRange, refs.enemyLayerMask))
        {
            //print("HIT ENEMY with weapon of ID '" + ID + "'");

            hit.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
            refs.generalPool.Dispatch(PoolType.vfx_blood, hit.point, Quaternion.identity);
        }
    }

    protected override void HandleRecoiAnim()
    {
        //base.HandleRecoiAnim();
        currentRecoilAnimFactor = Mathf.MoveTowards(currentRecoilAnimFactor, 1, recoilAnimSpeed * Time.deltaTime);

        pivotTrans.localPosition = Vector3.Lerp(
            pivotTransDefaultPos,
            swingAnimPosTarget,
            Mathf.PingPong(currentRecoilAnimFactor, 0.5f)
            );

        transform.localRotation = Quaternion.Euler(Vector3.Lerp(
            Vector3.zero,
            swingAnimRotTarget,
            Mathf.PingPong(currentRecoilAnimFactor, 0.5f)
            ));
    }
}
