using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_chicken : pl_wep_base
{
    [Header("CHICKEN --- REFS")]
    [SerializeField] LineRenderer beamLine;

    [Header("CHICKEN --- VALUES")]
    [SerializeField] float beamRange;
    [SerializeField] LayerMask beamLayerMask;

    private void Start()
    {
        beamLine.enabled = false;    
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKey(KeyCode.Mouse0) && currentEquipAnimFactor == 1 && g_refs.Instance.sessionData.wepInfoDict[wepType.chicken].ammo > 0)
        {
            beamLine.enabled = true;
            HandleBeamVisuals();
        }
        else
        {
            beamLine.enabled = false;
        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        RaycastHit hit;
        if (Physics.Raycast(refs.camHolderTrans.position, refs.camHolderTrans.forward, out hit, beamRange, beamLayerMask))
        {
            //print("HIT ENEMY with weapon of ID '" + ID + "'");
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
                refs.generalPool.Dispatch(PoolType.vfx_blood, hit.point, Quaternion.identity);
            }

            //refs.tracerPool.Dispatch(firePoint.position, hit.point, pl_wep_tracertype.Rifle);
        }
    }

    private void HandleBeamVisuals() // double raycast isnt very efficient and technically avoidable - but wep inheritance system doesn't allow for a clean implementation so idc, for now
    {
        beamLine.SetPosition(0, firePoint.position);

        RaycastHit hit;
        if (Physics.Raycast(refs.camHolderTrans.position, refs.camHolderTrans.forward, out hit, beamRange, beamLayerMask))
        {
            beamLine.SetPosition(1, hit.point);
        }
        else
        {
            beamLine.SetPosition(1, refs.camHolderTrans.position + refs.camHolderTrans.forward * beamRange);
        }
    }
}
