using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_wep_tracers_pool : MonoBehaviour
{
    [SerializeField] List<pl_wep_tracer> tracerObjectsRifle;
    [SerializeField] List<pl_wep_tracer> tracerObjectsShotgun;

    Dictionary<pl_wep_tracertype, pl_wep_tracerpoolentry> poolDict;

    private void Start()
    {
        poolDict = new Dictionary<pl_wep_tracertype, pl_wep_tracerpoolentry>();
        poolDict.Add(pl_wep_tracertype.Rifle, new pl_wep_tracerpoolentry(tracerObjectsRifle));
        poolDict.Add(pl_wep_tracertype.Shotgun, new pl_wep_tracerpoolentry(tracerObjectsShotgun));
    }

    public void Dispatch(Vector3 posFrom, Vector3 posTo, pl_wep_tracertype type)
    {
        pl_wep_tracer tracerObject = poolDict[type].objectList[poolDict[type].currentIDX];

        tracerObject.Init(posFrom, posTo);
        tracerObject.enabled = true;

        poolDict[type].currentIDX++;
        if(poolDict[type].currentIDX == poolDict[type].objectList.Count)
        {
            poolDict[type].currentIDX = 0;
        }
    }
}

public class pl_wep_tracerpoolentry
{
    public List<pl_wep_tracer> objectList;
    public int currentIDX;

    public pl_wep_tracerpoolentry(List<pl_wep_tracer> objectList)
    {
        this.objectList = objectList;
        currentIDX = 0;
    }
}

public enum pl_wep_tracertype
{
    Rifle,
    Shotgun
}
