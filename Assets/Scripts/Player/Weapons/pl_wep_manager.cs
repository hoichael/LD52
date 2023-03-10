using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_manager : MonoBehaviour
{
    [SerializeField] Transform camHolderTrans;
    //[SerializeField] List<pl_wep_base> weaponList;
    [SerializeField] pl_wep_base rifleScript, shotgunScript, launcherScript, axeScript, chickenScript;
    pl_wep_base activeWeaponScript;
    wepType activeWeaponType;

    [SerializeField] pl_ik_target_pos ikPosHandlerLeft, ikPosHandlerRight;
    [SerializeField] Transform ikTargetHolderDefLeft, ikTargetHolderDefRight;
    [SerializeField] Transform ikTargetLeft, ikTargetRight;
    [SerializeField] pl_wep_bob wepBobManager;
    [SerializeField] pl_wep_sway wepSwayManager;

    [SerializeField] GameObject humanArm;
    [SerializeField] SkinnedMeshRenderer rendererCornLeft, rendererCornRight;
    [SerializeField] GameObject defaultCrosshair;

    private void Start()
    {
        SetIKPosToDefault();
        if(g_refs.Instance.sessionData.wepInfoDict == null)
        {
            SetupWepInfoDict();
        }
        else
        {
            SetupWepUI();
        }
    }

    private void Update()
    {
        for (int i = 1; i < g_refs.Instance.sessionData.wepInfoDict.Count + 1; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                TryWeaponSwitch(i - 1, wepSwitchMode.JumpTo);
            }
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            TryWeaponSwitch((int)Mathf.Sign(Input.mouseScrollDelta.y), wepSwitchMode.CycleBy);
        }

        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.CapsLock))
        {
            TryWeaponSwitch(1, wepSwitchMode.CycleBy);
        }
    }

    public void SwitchWeapon(wepType type)
    {
        if (g_refs.Instance.sessionData.playerHP <= 0) return;

        defaultCrosshair.SetActive(false);

        pl_wep_base newWeapon = g_refs.Instance.sessionData.wepInfoDict[type].wepScript;

        if(newWeapon == activeWeaponScript) // accounts for both picking up already equipped weapon as well as invalid ID
        {
            return;
        }

        DropWeapon();

        activeWeaponType = type;

        if(!g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].owned)
        {
            g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].owned = true;
            g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].ammo = g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].wepScript.initAmmo;
        }
        
        g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].active = true;
        activeWeaponScript = newWeapon;
        activeWeaponScript.Equip();

        ParentIKTargets();

        wepBobManager.OnWeaponSwitch(activeWeaponScript.animWeightMult);
        wepSwayManager.OnWeaponSwitch(activeWeaponScript.pivotTransDefaultPos, activeWeaponScript.pivotTrans, activeWeaponScript.animWeightMult);

        g_refs.Instance.sfxOneshot2D.Play(SfxType.wep_switch);
    }

    public void DropWeapon()
    {
        if (activeWeaponScript == null) return;

        activeWeaponScript.Unequip();
        activeWeaponScript = null;

        g_refs.Instance.sessionData.wepInfoDict[activeWeaponType].active = false;
        activeWeaponType = wepType.NULL;

        SetIKPosToDefault();
    }

    private void TryWeaponSwitch(int indexOrDir, wepSwitchMode switchMode)
    {
        if (activeWeaponScript == null) return;

        // this whole function is utterly retarded but I do not care, for I have embraced the schizo code
        pl_wep_info[] wepInfoArr = new pl_wep_info[g_refs.Instance.sessionData.wepInfoDict.Count];
        int currentCheckIDX = 0;

        foreach(pl_wep_info wepInfo in g_refs.Instance.sessionData.wepInfoDict.Values)
        {
            wepInfoArr[wepInfo.carryOrder] = wepInfo;
            if(wepInfo.type == activeWeaponType)
            {
                currentCheckIDX = wepInfo.carryOrder;
            }
        }
        
        if(switchMode == wepSwitchMode.CycleBy)
        {
            for (int i = 0; i < wepInfoArr.Length; i++)
            {
                currentCheckIDX += indexOrDir;

                if (currentCheckIDX == wepInfoArr.Length)
                {
                    currentCheckIDX = 0;
                }
                else if (currentCheckIDX == -1)
                {
                    currentCheckIDX = wepInfoArr.Length - 1;
                }

                if (wepInfoArr[currentCheckIDX].owned)
                {
                    SwitchWeapon(wepInfoArr[currentCheckIDX].type);
                    return;
                }
            }
        }
        else
        {
            if (wepInfoArr[indexOrDir].owned)
            {
                SwitchWeapon(wepInfoArr[indexOrDir].type);
            }
        }
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
        freshWepInfoDict.Add(wepType.axe, new pl_wep_info(wepType.axe, axeScript, 0));
        freshWepInfoDict.Add(wepType.rifle, new pl_wep_info(wepType.rifle, rifleScript, 1));
        freshWepInfoDict.Add(wepType.shotgun, new pl_wep_info(wepType.shotgun, shotgunScript, 2));
        freshWepInfoDict.Add(wepType.launcher, new pl_wep_info(wepType.launcher, launcherScript, 3));
        freshWepInfoDict.Add(wepType.chicken, new pl_wep_info(wepType.chicken, chickenScript, 4));

        g_refs.Instance.sessionData.wepInfoDict = freshWepInfoDict;
    }

    private void SetupWepUI()
    {
        // doesnt belong here but its fine for now
        humanArm.SetActive(false);
        rendererCornLeft.enabled = true;
        rendererCornRight.enabled = true;

        // doesnt belong here but its fine for now
        g_refs.Instance.sessionData.wepInfoDict[wepType.axe].wepScript = axeScript;
        g_refs.Instance.sessionData.wepInfoDict[wepType.rifle].wepScript = rifleScript;
        g_refs.Instance.sessionData.wepInfoDict[wepType.shotgun].wepScript = shotgunScript;
        g_refs.Instance.sessionData.wepInfoDict[wepType.launcher].wepScript = launcherScript;
        g_refs.Instance.sessionData.wepInfoDict[wepType.chicken].wepScript = chickenScript;

        foreach (pl_wep_info wepInfo in g_refs.Instance.sessionData.wepInfoDict.Values)
        {
            if(wepInfo.owned)
            {
                wepInfo.wepScript.uiObject.SetActive(true);
                wepInfo.wepScript.UpdateAmmoUI();
                if (wepInfo.active)
                {
                    wepInfo.wepScript.uiRotator.enabled = true;

                    // doesnt belong here but its fine for now
                    SwitchWeapon(wepInfo.type);
                }
            }
        }
    }
}

public class pl_wep_info
{
    public wepType type;
    public pl_wep_base wepScript;
    public bool owned;
    public bool active;
    public int ammo;
    public int carryOrder;

    public pl_wep_info(wepType type, pl_wep_base wepScript, int carryOrder)
    {
        this.type = type;
        this.wepScript = wepScript;
        this.carryOrder = carryOrder;
        owned = active = false;
        ammo = 0;
    }
}

public enum wepType
{
    NULL,
    rifle,
    shotgun,
    launcher,
    axe,
    chicken
}

public enum wepSwitchMode
{
    CycleBy,
    JumpTo
}
