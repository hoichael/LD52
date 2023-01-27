using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nv_monitor : MonoBehaviour
{
    [SerializeField] List<string> promptsList;
    [SerializeField] TextMeshPro promptTextEl;
    [SerializeField] GameManager gameManager;

    [SerializeField] music_manager musicManager;

    //[SerializeField] AudioSource audioSource;

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

        if (currentPromptIDX != 0)
        {
            //audioSource.Play();
            g_refs.Instance.sfxOneshot2D.Play(SfxType.button_a);
        }

        if (currentPromptIDX == 1)
        {
            musicManager.OnInitButtonPress();

            if (g_refs.Instance.sessionData.currentWaveRegular > 0)
            {
                gameManager.HarvestTime();
                return;
            }
            else
            {
                gameManager.InitHarvestTimer();
                return;
            }
        }

        if(currentPromptIDX == promptsList.Count)
        {
            currentPromptIDX -= 2;
        }

        promptTextEl.text = promptsList[currentPromptIDX];
    }

    public void HandleHarvestTime()
    {
        harvestTime = true;
        promptTextEl.text = "HARVEST TIME";
        promptTextEl.color = Color.red;
    }
}
