using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_manager : MonoBehaviour
{
    [SerializeField] Transform camHolderTrans;
    //[SerializeField] List<pl_wep_base> weaponList;
    [SerializeField] pl_wep_base rifleScript, shotgunScript, launcherScript;
    pl_wep_base activeWeaponScript;
    wepType activeWeaponType;

    [SerializeField] pl_ik_target_pos ikPosHandlerLeft, ikPosHandlerRight;
    [SerializeField] Transform ikTargetHolderDefLeft, ikTargetHolderDefRight;
    [SerializeField] Transform ikTargetLeft, ikTargetRight;
    [SerializeField] pl_wep_bob wepBobManager;
    [SerializeField] pl_wep_sway wepSwayManager;

    private void Start()
    {
        SetIKPosToDefault();
        if(g_refs.Instance.sessionData.wepInfoDict == null)
        {
            SetupWepInfoDict();
        }
    }

    public void PickupWeapon(wepType type)
    {
        pl_wep_base newWeapon = g_refs.Instance.sessionData.wepInfoDict[type].wepScript;

        if(newWeapon == activeWeaponScript) // accounts for both picking up already equipped weapon as well as invalid ID
        {
            return;
        }

        DropWeapon();

        activeWeaponType = type;
        g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].owned = true;
        g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].active = true;
        activeWeaponScript = newWeapon;
        activeWeaponScript.Equip();

        ParentIKTargets();

        wepBobManager.OnWeaponSwitch(activeWeaponScript.animWeightMult);
        wepSwayManager.OnWeaponSwitch(activeWeaponScript.pivotTransDefaultPos, activeWeaponScript.pivotTrans, activeWeaponScript.animWeightMult);
    }

    public void DropWeapon()
    {
        if (activeWeaponScript == null) return;

        activeWeaponScript.Drop();
        activeWeaponScript = null;

        g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].active = false;
        activeWeaponType = wepType.NULL;

        SetIKPosToDefault();
    }

    private void ParentIKTargets()
    {
        ikTargetLeft.SetParent(activeWeaponScript.ikTargetHolderLeft);
        ikTargetLeft.localPosition = Vector3.zero;

        ikTargetRight.SetParent(activeWeaponScript.ikTargetHolderRight);
        ikTargetRight.localPosition = Vector3.zero;

        //ikPosHandlerLeft.currentTargetTrans = activeWeapon.ikTargetHolderLeft;
        //ikPosHandlerRight.currentTargetTrans = activeWeapon.ikTargetHolderRight;
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

    private void SetupWepInfoDict()
    {
        Dictionary<wepType, pl_wep_info> freshWepInfoDict = new Dictionary<wepType, pl_wep_info>();
        freshWepInfoDict.Add(wepType.rifle, new pl_wep_info(rifleScript));
        freshWepInfoDict.Add(wepType.shotgun, new pl_wep_info(shotgunScript));
        freshWepInfoDict.Add(wepType.launcher, new pl_wep_info(launcherScript));

        g_refs.Instance.sessionData.wepInfoDict = freshWepInfoDict;
    }
}

public class pl_wep_info
{
    public pl_wep_base wepScript;
    public bool owned;
    public bool active;
    public int ammo;

    public pl_wep_info(pl_wep_base wepScript)
    {
        this.wepScript = wepScript;
        owned = active = false;
        ammo = 0;
    }
}

public enum wepType
{
    NULL,
    rifle,
    shotgun,
    launcher
}
