using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te_selectable_base : MonoBehaviour
{
    [SerializeField] protected te_manager terminalManager;
    public string slTextUpper, slTextLower;

    public virtual void Select()
    {
        terminalManager.textElUpper.text = slTextUpper;
        terminalManager.textElLower.text = slTextLower;
    }

    public virtual void Use()
    {

    }
}
