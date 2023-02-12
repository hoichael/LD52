using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class g_audiovolume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] float exitFadeSpeed;
    float fadeStartVolume;
    float currentFadeFactor;

    private void Start()
    {
        SetMixerVolume("volParamMusic", g_refs.Instance.sessionData.audioVolumeMusic);
        SetMixerVolume("volParamSFX", g_refs.Instance.sessionData.audioVolumeSFX);

        mixer.SetFloat("volParamMaster", 0);
    }

    public void SetMixerVolume(string mixerKey, int volumeValue)
    {
        float processedBaseValue = Mathf.Clamp(volumeValue * 0.1f, 0.0001f, 1);
        mixer.SetFloat(mixerKey, Mathf.Log10(processedBaseValue) * 20);

        //g_refs.Instance.sfxOneshot2D.Play(SfxType.pickup);
    }

    private void Update()
    {
        if (currentFadeFactor != 0)
        {
            HandleFadeout();
        }
    }

    private void HandleFadeout()
    {
        currentFadeFactor = Mathf.MoveTowards(currentFadeFactor, 0, exitFadeSpeed * Time.deltaTime);

        //musicSrc.volume = Mathf.Lerp(
        //    0,
        //    defaultVolume,
        //    currentFadeFactor
        //    );

        mixer.SetFloat("volParamMaster", Mathf.Lerp(
            -80,
            fadeStartVolume,
            currentFadeFactor
            ));
    }

    public void OnSceneExit()
    {
        currentFadeFactor = 1;
        mixer.GetFloat("volParamMaster", out fadeStartVolume);
    }
}
