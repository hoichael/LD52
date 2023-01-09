using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float harvestTimerTime;
    [SerializeField] ui_Manager uiManager;
    [SerializeField] nv_monitor monitorManager;
    [SerializeField] nv_office_collapse officeCollapser;

    [SerializeField] float containerEnableDelay;

    [SerializeField] GameObject enemiesContainer;
    [SerializeField] GameObject weaponsContainer;

    public void InitHarvestTimer()
    {
        StartCoroutine(HandleHarvestTimer());
    }

    private void HarvestTime()
    {
        print("HARVEST TIME");
        monitorManager.HandleHarvestTime();
        uiManager.HandleHarvestTime();
        officeCollapser.HandleHarvestTime();

    }

    private IEnumerator HandleHarvestTimer()
    {
        yield return new WaitForSeconds(harvestTimerTime);
        HarvestTime();

        yield return new WaitForSeconds(containerEnableDelay);
        enemiesContainer.SetActive(true);
        weaponsContainer.SetActive(true);
    }
}
