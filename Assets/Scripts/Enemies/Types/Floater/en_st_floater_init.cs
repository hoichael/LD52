using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_floater_init : en_state_base
{
    [SerializeField] float chaseSwitchDelay;
    [SerializeField] float spawnImpulseForce;
    protected override void OnEnable()
    {
        base.OnEnable();
        HandleSpawnImpulse();
        StartCoroutine(DelayedChaseSwitch());
    }

    private void HandleSpawnImpulse()
    {
        Vector3 impulseDir = Random.insideUnitSphere;
        info.rb.AddForce(impulseDir * spawnImpulseForce, ForceMode.Impulse);
    }

    private IEnumerator DelayedChaseSwitch()
    {
        yield return new WaitForSeconds(chaseSwitchDelay);
        ChangeState("chase");
    }
}
