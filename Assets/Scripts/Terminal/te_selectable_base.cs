using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class te_selectable_base : MonoBehaviour
{
    [SerializeField] protected te_manager terminalManager;
    public string slTextUpper/*, slTextLower*/;
    [SerializeField] protected int cost;
    [SerializeField] protected TextMeshPro cashTextEl; 

    public virtual void Select()
    {
        terminalManager.textElUpper.text = slTextUpper;
        //terminalManager.textElLower.text = slTextLower;
        terminalManager.textElLower.text = "Cost: " + cost;
    }

    public virtual void Use()
    {
        //if (g_refs.Instance.sessionData.cash < cost)
        //{
        //    Deny();
        //    return;
        //}
    }

    protected virtual void Deny()
    {
        print("DENIED");
    }
}
