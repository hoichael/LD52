using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_manager : MonoBehaviour
{
    [SerializeField] AudioSource musicSrc;
    [SerializeField] float fadeSpeed;

    float currentFadeFactor;
    float defaultVolume;

    public void OnInitButtonPress()
    {
        musicSrc.Play();

        if (g_refs.Instance.sessionData.currentWaveRegular != 0)
        {
            musicSrc.time = 28.7f;
        }
    }

    private void Update()
    {
        if(currentFadeFactor != 0)
        {
            HandleFadeout();
        }
    }

    private void HandleFadeout()
    {
        currentFadeFactor = Mathf.MoveTowards(currentFadeFactor, 0, fadeSpeed * Time.deltaTime);
        musicSrc.volume = Mathf.Lerp(
            0,
            defaultVolume,
            currentFadeFactor
            );
    }

    public void OnWaveComplete()
    {
        currentFadeFactor = 1;
        defaultVolume = musicSrc.volume;
    }
}
