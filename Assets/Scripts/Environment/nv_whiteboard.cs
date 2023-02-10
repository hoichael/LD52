using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class nv_whiteboard : MonoBehaviour
{
    [SerializeField] nv_int_whiteboard[] intSensArr, intVolMusicArr, intVolSFXArr;
    [SerializeField] g_mousesens sensManager;
    [SerializeField] g_audiovolume volumeManager;

    Dictionary<int_whiteboard_type, nv_int_whiteboard[]> intDict;

    private void Awake()
    {
        intDict = new Dictionary<int_whiteboard_type, nv_int_whiteboard[]>();
        intDict.Add(int_whiteboard_type.sens, intSensArr);
        intDict.Add(int_whiteboard_type.volMusic, intVolMusicArr);
        intDict.Add(int_whiteboard_type.volSFX, intVolSFXArr);
    }

    private void Start()
    {
        HandleIteration(intSensArr, intSensArr[g_refs.Instance.sessionData.mouseSensLevel]);
        HandleIteration(intVolMusicArr, intVolMusicArr[g_refs.Instance.sessionData.audioVolumeMusic]);
        HandleIteration(intVolSFXArr, intVolSFXArr[g_refs.Instance.sessionData.audioVolumeSFX]);
    }

    public void HandleInteract(nv_int_whiteboard intObj)
    {
        int value = HandleIteration(intDict[intObj.type], intObj);

        if (intObj.type == int_whiteboard_type.sens)
        {
            g_refs.Instance.sessionData.mouseSensLevel = value;
            sensManager.SetMouseSens();
        }
        else if(intObj.type == int_whiteboard_type.volMusic)
        {
            g_refs.Instance.sessionData.audioVolumeMusic = value;
            volumeManager.SetMixerVolume("volParamMusic", g_refs.Instance.sessionData.audioVolumeMusic);
        }
        else
        {
            g_refs.Instance.sessionData.audioVolumeSFX = value;
            volumeManager.SetMixerVolume("volParamSFX", g_refs.Instance.sessionData.audioVolumeSFX);
        }
    }

    private int HandleIteration(nv_int_whiteboard[] intArr, nv_int_whiteboard intObj)
    {
        int value = 0;

        foreach(nv_int_whiteboard entry in intArr)
        {
            entry.enabled = false;
        }

        while(value < intArr.Length)
        {
            intArr[value].enabled = true;
            if (object.ReferenceEquals(intArr[value], intObj))
            {
                break;
            }
            value++;
        }

        return value;
    }
}

public enum int_whiteboard_type
{
    sens,
    volMusic,
    volSFX
}
