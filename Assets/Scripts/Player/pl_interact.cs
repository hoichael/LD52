using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_interact : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] LayerMask intLayerMask;
    [SerializeField] Transform camHolderTrans;
    [SerializeField] TextMeshPro uiTextElHover;
    [SerializeField] TextMeshPro uiTextElUpper;
    [SerializeField] pl_wep_manager weaponManager;
    [SerializeField] pl_upgrade_manager upgradeManager;
    [SerializeField] nv_monitor monitorManager;
    nv_int_base hoverInteractable;
    
    private void Update()
    {
        CheckForInteractable();

        if(hoverInteractable != null)
        {
            HandleHover();
        }
    }

    private void CheckForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(camHolderTrans.position, camHolderTrans.forward, out hit, refs.settings.interactRange, intLayerMask))
        {
            hoverInteractable = hit.transform.GetComponent<nv_int_base>();
        }
        else
        {
            hoverInteractable = null;
            uiTextElHover.text = "";
        }
    }

    private void HandleHover()
    {
        //print(hoverInteractable.hoverText);
        uiTextElHover.text = hoverInteractable.hoverText;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(hoverInteractable.weaponID != "")
            {
                weaponManager.PickupWeapon(hoverInteractable.weaponID);
            }
            else if (hoverInteractable.upgradeKey != "")
            {
                pl_upgrade_message msg = upgradeManager.TryUpgrade(hoverInteractable.upgradeKey);
                
                uiTextElUpper.text = msg.message;
                StopAllCoroutines();
                StartCoroutine(HandleUpperTextDuration());

            }
            else if(hoverInteractable.isMonitorButton)
            {
                monitorManager.HandleButtonPress();
            }
            hoverInteractable.HandleInteract();
        }
    }

    private IEnumerator HandleUpperTextDuration()
    {
        yield return new WaitForSeconds(2);
        uiTextElUpper.text = "";
    }
}
