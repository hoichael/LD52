using System.Collections.Generic;
using UnityEngine;

public class lv_pool : MonoBehaviour
{
    [SerializeField] List<lv_pool_entry_config> poolInfoList;
    [SerializeField] Transform outerContainer;
    Dictionary<PoolType, lv_pool_entry> poolDict;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        poolDict = new Dictionary<PoolType, lv_pool_entry>();

        foreach (lv_pool_entry_config config in poolInfoList)
        {
            lv_pool_entry newEntry = new lv_pool_entry();
            newEntry.amount = config.amount;
            newEntry.objArr = new GameObject[config.amount];

            GameObject container = new GameObject();
            container.name = config.name;
            container.transform.SetParent(outerContainer);
            newEntry.container = container.transform;

            for (int i = 0; i < config.amount; i++)
            {
                GameObject obj = Instantiate(config.prefab);
                obj.transform.SetParent(container.transform);
                obj.SetActive(false);
                newEntry.objArr[i] = obj;
            }

            poolDict.Add(config.key, newEntry);
        }
    }

    public void Dispatch(PoolType key, Vector3 pos, Quaternion rot)
    {
        lv_pool_entry poolEntry = poolDict[key];

        GameObject objToDispatch = poolEntry.objArr[poolEntry.currentIDX];

        objToDispatch.SetActive(false); // disable in case currentIDX is looping to objects that are still active in scene

        objToDispatch.transform.SetParent(null);

        objToDispatch.transform.localPosition = pos;
        objToDispatch.transform.localRotation = rot;
        objToDispatch.SetActive(true);
        IncrementIDX(poolEntry);
    }

    public void Return(PoolType key, Transform objTrans, bool active)
    {
        objTrans.SetParent(poolDict[key].container);
        objTrans.gameObject.SetActive(active);
    }

    public void DisableAll(PoolType key)
    {
        foreach (GameObject obj in poolDict[key].objArr)
        {
            obj.SetActive(false);
        }
    }

    private void IncrementIDX(lv_pool_entry poolEntry)
    {
        poolEntry.currentIDX++;
        if (poolEntry.currentIDX == poolEntry.amount)
        {
            poolEntry.currentIDX = 0;
        }
    }
}

public enum PoolType
{
    en_walker,
    en_giant,
    vfx_blood,
    vfx_blood_expl,
    vfx_muzzleflash,
    proj_launcher,
    launcher_explosion,
    vfx_launcher_air,
    en_shooter,
    proj_en_shooter,
    vfx_mflash_en_shooter,
    vfx_jump_small,
    vfx_jump_big,
    en_exploder,
    en_exploder_explosion,
    drop_health,
    drop_cash,
    vfx_dust_spawn,
    vfx_dust_run
}

[System.Serializable]
public class lv_pool_entry_config
{
    public PoolType key;
    public GameObject prefab;
    public int amount;
    public string name;
}

public class lv_pool_entry
{
    public GameObject[] objArr;
    public Transform container;
    public int amount;
    public int currentIDX;
}