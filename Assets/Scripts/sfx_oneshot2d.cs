using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_oneshot2d : MonoBehaviour
{
    [SerializeField] sfx_info[] sfxList;


    public void Play(SfxType type)
    {
        foreach(sfx_info sfxInfo in sfxList)
        {
            if(type == sfxInfo.type)
            {
                sfxInfo.src.Play();
            }
        }
    }
}

[System.Serializable]
public class sfx_info
{
    public SfxType type;
    public AudioSource src;
}

public enum SfxType
{
    rifle_shoot,
    shotgun_shoot,
    launcher_shoot,

    player_hurt,

    error,

    button_a,
    button_b,

    pickup,
    wep_switch,

    ammo_get,
    
    axe_swing
}
