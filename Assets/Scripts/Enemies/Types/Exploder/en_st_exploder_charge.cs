using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_exploder_charge : en_state_base
{
    [SerializeField] float duration;
    [SerializeField] AudioSource chargeAudioSrc;
    protected override void OnEnable()
    {
        base.OnEnable();
        chargeAudioSrc.Play();
        info.anim.CrossFade("Explode", 0.2f);
        StartCoroutine(HandleDuration());
    }

    private IEnumerator HandleDuration()
    {
        yield return new WaitForSeconds(duration);
        
        // this is kinda fucky
        g_refs.Instance.gameManager.UpdateScore(100);
        g_refs.Instance.waveManager.HandleEnemyDeath();
        
        ChangeState("Explode");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }
}
