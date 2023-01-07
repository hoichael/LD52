using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_manager : MonoBehaviour
{
    [SerializeField] Transform camHolderTrans;
    [SerializeField] List<pl_wep_base> weaponList;
    pl_wep_base activeWeapon;


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
    }

    private void DropWeapon()
    {
        if (activeWeapon == null) return;
        activeWeapon.Drop();
        activeWeapon = null;
    }
}
