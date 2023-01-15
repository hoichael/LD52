using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_proj_launcher_explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] float lifeTime;
    [SerializeField] float colRadius;
    [SerializeField] LayerMask enemyMask, playerMask;

    private void OnEnable()
    {
        particles.Play();
        StartCoroutine(HandleLifetime());
        HandleCol();
    }

    private void HandleCol()
    {
        Collider[] hitCols = Physics.OverlapSphere(transform.position, colRadius, enemyMask);
        HandleHitEnemies(hitCols);

        if(Physics.CheckSphere(transform.position, colRadius, playerMask) != false)
        {
            HandleHitPlayer();
        }
    }

    private void HandleHitEnemies(Collider[] hitCols)
    {
        print("HIT ENEMY AMOUNT: " + hitCols.Length);

        foreach(Collider col in hitCols)
        {

        }
    }

    private void HandleHitPlayer()
    {
        print("HIT PLAYER");
    }

    private IEnumerator HandleLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        g_refs.Instance.pool.Return(PoolType.launcher_explosion, transform, false);
    }
}
