using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_tracers_pool : MonoBehaviour
{
    [SerializeField] List<pl_wep_tracer> tracerObjectsRifle;
    [SerializeField] List<pl_wep_tracer> tracerObjectsShotgun;


    int currentIDXRifle;
    int currentIDXShotgun;


    public void Dispatch(Vector3 posFrom, Vector3 posTo, pl_wep_tracertype type)
    {
        // temp, will implement proper dict based thing
        if(type == pl_wep_tracertype.Rifle)
        {
            tracerObjectsRifle[currentIDXRifle].Init(posFrom, posTo);
            tracerObjectsRifle[currentIDXRifle].enabled = true;

            currentIDXRifle++;
            if (currentIDXRifle == tracerObjectsRifle.Count)
            {
                currentIDXRifle = 0;
            }
        }
        else
        {
            tracerObjectsShotgun[currentIDXShotgun].Init(posFrom, posTo);
            tracerObjectsShotgun[currentIDXShotgun].enabled = true;

            currentIDXShotgun++;
            if (currentIDXShotgun == tracerObjectsShotgun.Count)
            {
                currentIDXShotgun = 0;
            }
        }
    }
}

public enum pl_wep_tracertype
{
    Rifle,
    Shotgun
}
