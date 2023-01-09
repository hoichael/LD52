using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_walker_hit : en_state_base
{
    [SerializeField] float hitDuration;
    [SerializeField] List<AudioClip> clipList;
    [SerializeField] AudioSource audioSource;

    protected override void OnEnable()
    {
        base.OnEnable();
        info.anim.CrossFade("Hit", 0.1f);

        StartCoroutine(HandleDuration());


        int randomIDX = Random.Range(0, clipList.Count);
        audioSource.PlayOneShot(clipList[randomIDX]);
    }

    private IEnumerator HandleDuration()
    {
        yield return new WaitForSeconds(hitDuration);
        ChangeState("idle");
    }
}
