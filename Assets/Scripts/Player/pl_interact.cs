using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_interact : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] LayerMask intLayerMask;
    [SerializeField] Transform camHolderTrans;
    [SerializeField] TextMeshPro uiTextElement;
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
            uiTextElement.text = "";
        }
    }

    private void HandleHover()
    {
        //print(hoverInteractable.hoverText);
        uiTextElement.text = hoverInteractable.hoverText;

        if(Input.GetKeyDown(KeyCode.E))
        {
            hoverInteractable.HandleInteract();
        }
    }
}
