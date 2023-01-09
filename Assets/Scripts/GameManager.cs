using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float harvestTimerTime;
    [SerializeField] ui_Manager uiManager;
    [SerializeField] nv_monitor monitorManager;
    [SerializeField] nv_office_collapse officeCollapser;

    [SerializeField] float containerEnableDelay;

    [SerializeField] GameObject enemiesContainer;
    [SerializeField] GameObject weaponsContainer;

    [SerializeField] TextMeshPro scoreText;
    int score;

    private void Start()
    {
        UpdateScore(0);
    }

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

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
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
