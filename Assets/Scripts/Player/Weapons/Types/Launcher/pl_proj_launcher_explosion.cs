using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_proj_launcher_explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] float lifeTime;
    [SerializeField] float colRadius;
    [SerializeField] LayerMask enemyMask, playerMask;

    [SerializeField] dmg_info dmgInfo;
    [SerializeField] AudioSource audioSrc;

    [Header("Rocket Jump")]
    [SerializeField] float maxRJCheckDist;
    [SerializeField] float maxRJkForce, minRJForce;
    [SerializeField] float rjUpwardsForceBase;

    private void OnEnable()
    {
        particles.Play();
        StartCoroutine(HandleLifetime());
        HandleCol();
        audioSrc.Play();
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
        //print("HIT ENEMY AMOUNT: " + hitCols.Length);

        foreach(Collider col in hitCols)
        {
            col.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
        }
    }

    private void HandleHitPlayer()
    {
        Vector3 camPos = g_refs.Instance.plTrans.position + new Vector3(0, 0.3f, 0);  // kinda scuffed, approximate player cam pos

        float dist = Vector3.Distance(transform.position, camPos);
        if (dist > maxRJCheckDist) return;

        Vector3 dir = camPos - transform.position;

        float force = Mathf.Lerp(
            minRJForce,
            maxRJkForce,
            maxRJCheckDist / dist
            );

        g_refs.Instance.plRB.AddForce(dir * force, ForceMode.Impulse);
        g_refs.Instance.plRB.AddForce(Vector3.up * (rjUpwardsForceBase * force), ForceMode.Impulse);
    }

    private IEnumerator HandleLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        g_refs.Instance.pool.Return(PoolType.launcher_explosion, transform, false);
    }
}
