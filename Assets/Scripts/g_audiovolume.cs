using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class g_audiovolume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    private void Start()
    {
        SetMixerVolume("volParamMusic", g_refs.Instance.sessionData.audioVolumeMusic);
        SetMixerVolume("volParamSFX", g_refs.Instance.sessionData.audioVolumeSFX);
    }

    public void SetMixerVolume(string mixerKey, int volumeValue)
    {
        float processedBaseValue = Mathf.Clamp(volumeValue * 0.1f, 0.0001f, 1);
        mixer.SetFloat(mixerKey, Mathf.Log10(processedBaseValue) * 20);

        g_refs.Instance.sfxOneshot2D.Play(SfxType.pickup);
    }
}
