using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float harvestTimerTime;
    [SerializeField] ui_Manager uiManager;
    [SerializeField] nv_monitor monitorManager;

    public void InitHarvestTimer()
    {
        StartCoroutine(HandleHarvestTimer());
    }

    private void HarvestTime()
    {
        print("HARVEST TIME");
        monitorManager.HandleHarvestTime();
    }

    private IEnumerator HandleHarvestTimer()
    {
        yield return new WaitForSeconds(harvestTimerTime);
        HarvestTime();
    }
}
