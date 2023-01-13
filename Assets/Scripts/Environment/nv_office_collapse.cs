using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nv_office_collapse : MonoBehaviour
{
    [SerializeField] Transform[] wallArr = new Transform[4];
    [SerializeField] Transform ceiling;
    [SerializeField] BoxCollider floorCol;

    [SerializeField] float collapseSpeed;
    float currentCollapseFactor = 1;

    [SerializeField] Vector3 ceilingTargetPos;
    Vector3 ceilingStartPos;

    public void HandleHarvestTime()
    {
        currentCollapseFactor = 0;
        ceilingStartPos = ceiling.localPosition;
        ceiling.GetComponent<BoxCollider>().enabled = false;

        floorCol.enabled = false;
    }

    private void Update()
    {
        if(currentCollapseFactor != 1)
        {
            HandleCollapseAnim();
        }
    }

    private void HandleCollapseAnim()
    {
        currentCollapseFactor = Mathf.MoveTowards(currentCollapseFactor, 1, collapseSpeed * Time.deltaTime);

        float wallRotZ = Mathf.Lerp(
            0,
            90,
            currentCollapseFactor
            );

        ceiling.localPosition = Vector3.Lerp(
            ceilingStartPos,
            ceilingTargetPos,
            currentCollapseFactor
            );

        ceiling.localScale = Vector3.Lerp(
            new Vector3(10, 0.25f, 10),
            Vector3.zero,
            currentCollapseFactor
            );

        foreach (Transform wall in wallArr)
        {
            wall.localRotation = Quaternion.Euler(0, wall.localEulerAngles.y, wallRotZ);

            if (currentCollapseFactor == 1)
            {
                wall.GetComponentInChildren<BoxCollider>().enabled = false;
            }
        }
    }
}
