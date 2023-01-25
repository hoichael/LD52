using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class te_manager : MonoBehaviour
{
    [Header("REFS")]
    public TextMeshPro textElUpper;
    public TextMeshPro textElLower;
    [SerializeField] te_selectable_base[] selectables;


    [Header("VALUES")]
    [SerializeField] Vector3 slIconScaleDef;
    [SerializeField] Vector3 slIconScaleActive;
    [SerializeField] float xPosLeft, xPosMid, xPosRight;

    int activeSlIdx = -1;

    private void Awake()
    {
        //activeSelectable = selectables[0];
        foreach(te_selectable_base sl in selectables)
        {
            sl.gameObject.SetActive(false);
        }
        SwitchSelectable(1);
    }

    public void HandleButtonPress(int id)
    {
        if(id == 0)
        {
            selectables[activeSlIdx].Use();
        }
        else
        {
            SwitchSelectable(id);
        }
    }

    private void SwitchSelectable(int dirMult)
    {
        if (activeSlIdx + dirMult == -1 || activeSlIdx + dirMult == selectables.Length) return;

        foreach (te_selectable_base sl in selectables)
        {
            sl.gameObject.SetActive(false);
        }

        activeSlIdx += dirMult;
        selectables[activeSlIdx].gameObject.SetActive(true);
        selectables[activeSlIdx].transform.localScale = slIconScaleActive;
        selectables[activeSlIdx].transform.localPosition = new Vector3(xPosMid, selectables[activeSlIdx].transform.localPosition.y, selectables[activeSlIdx].transform.localPosition.z);
        selectables[activeSlIdx].Select();

        if (activeSlIdx != 0)
        {
            selectables[activeSlIdx - 1].gameObject.SetActive(true);
            selectables[activeSlIdx - 1].transform.localScale = slIconScaleDef;
            selectables[activeSlIdx - 1].transform.localPosition = new Vector3(xPosLeft, selectables[activeSlIdx].transform.localPosition.y, selectables[activeSlIdx].transform.localPosition.z);
        }

        if(activeSlIdx != selectables.Length - 1)
        {
            selectables[activeSlIdx + 1].gameObject.SetActive(true);
            selectables[activeSlIdx + 1].transform.localScale = slIconScaleDef;
            selectables[activeSlIdx + 1].transform.localPosition = new Vector3(xPosRight, selectables[activeSlIdx].transform.localPosition.y, selectables[activeSlIdx].transform.localPosition.z);
        }
    }



}
