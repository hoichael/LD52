using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_manager : MonoBehaviour
{
    [SerializeField] Transform camHolderTrans;
    [SerializeField] List<pl_wep_base> weaponList;
    pl_wep_base activeWeapon;

    [SerializeField] pl_ik_target_pos ikPosHandlerLeft, ikPosHandlerRight;
    [SerializeField] Transform ikTargetHolderDefLeft, ikTargetHolderDefRight;
    [SerializeField] Transform ikTargetLeft, ikTargetRight;
    [SerializeField] pl_wep_bob wepBobManager;
    [SerializeField] pl_wep_sway wepSwayManager;

    private void Start()
    {
        SetIKPosToDefault();
    }

    public void PickupWeapon(string ID)
    {
        pl_wep_base newWeapon = activeWeapon;

        foreach(pl_wep_base wep in weaponList)
        {
            if(ID == wep.ID)
            {
                newWeapon = wep;
                break;
            }
        }

        if(newWeapon == activeWeapon) // accounts for both picking up already equipped weapon as well as invalid ID
        {
            return;
        }

        DropWeapon();
        activeWeapon = newWeapon;
        activeWeapon.Equip();

        ParentIKTargets();

        wepBobManager.OnWeaponSwitch(activeWeapon.animWeightMult);
        wepSwayManager.OnWeaponSwitch(activeWeapon.pivotTransDefaultPos, activeWeapon.pivotTrans, activeWeapon.animWeightMult);
    }

    private void ParentIKTargets()
    {
        ikTargetLeft.SetParent(activeWeapon.ikTargetHolderLeft);
        ikTargetLeft.localPosition = Vector3.zero;

        ikTargetRight.SetParent(activeWeapon.ikTargetHolderRight);
        ikTargetRight.localPosition = Vector3.zero;

        //ikPosHandlerLeft.currentTargetTrans = activeWeapon.ikTargetHolderLeft;
        //ikPosHandlerRight.currentTargetTrans = activeWeapon.ikTargetHolderRight;
    }

    public void DropWeapon()
    {
        if (activeWeapon == null) return;
        activeWeapon.Drop();
        activeWeapon = null;

        SetIKPosToDefault();
    }

    private void SetIKPosToDefault()
    {
        ikTargetLeft.SetParent(ikTargetHolderDefLeft);
        ikTargetLeft.localPosition = Vector3.zero;

        ikTargetRight.SetParent(ikTargetHolderDefRight);
        ikTargetRight.localPosition = Vector3.zero;

        //ikPosHandlerLeft.currentTargetTrans = ikTargetHolderDefLeft;
        //ikPosHandlerRight.currentTargetTrans = ikTargetHolderDefRight;
    }
}
