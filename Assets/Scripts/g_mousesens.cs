using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_mousesens : MonoBehaviour
{
    [SerializeField] pl_settings playerSettings;
    [SerializeField] float maxSens = 2.5f;

    void Start()
    {
        SetMouseSens();
    }

    public void SetMouseSens()
    {
        playerSettings.mouseSens = maxSens * ((g_refs.Instance.sessionData.mouseSensLevel + 1) * 0.1f);
    }
}
