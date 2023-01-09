using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nv_monitor : MonoBehaviour
{
    [SerializeField] List<string> promptsList;
    [SerializeField] TextMeshPro promptTextEl;
    [SerializeField] GameManager gameManager;

    public int currentPromptIDX = -1;
    bool harvestTime;

    private void Start()
    {
        HandleButtonPress();
    }

    public void HandleButtonPress()
    {
        if (harvestTime) return;

        currentPromptIDX++;

        if(currentPromptIDX == 1)
        {
            gameManager.InitHarvestTimer();
        }

        if(currentPromptIDX == promptsList.Count)
        {
            currentPromptIDX -= 2;
        }

        promptTextEl.text = promptsList[currentPromptIDX];
    }

    public void HandleHarvestTime()
    {
        promptTextEl.text = "HARVEST TIME";
        promptTextEl.color = Color.red;
    }
}
