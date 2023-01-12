using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_tracers_pool : MonoBehaviour
{
    [SerializeField] List<pl_wep_tracer> tracerObjectsList;

    int currentIDX;


    public void Dispatch(Vector3 posFrom, Vector3 posTo)
    {
        tracerObjectsList[currentIDX].Init(posFrom, posTo);
        tracerObjectsList[currentIDX].enabled = true;

        currentIDX++;
        if(currentIDX == tracerObjectsList.Count)
        {
            currentIDX = 0;
        }
    }
}
