using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_exploder_explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] float lifeTime;
    [SerializeField] float colRadius;
    [SerializeField] float dmgDist;

    [SerializeField] LayerMask enemyMask, playerMask;

    [SerializeField] dmg_info dmgInfo;

    [SerializeField] float knockbackForce;

    private void OnEnable()
    {
        particles.Play();
        StartCoroutine(HandleLifetime());
        HandleCol();
    }

    private void HandleCol()
    {
        Collider[] hitCols = Physics.OverlapSphere(transform.position, colRadius, enemyMask);

        foreach (Collider col in hitCols)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < dmgDist)
            {
                col.transform.GetComponent<en_health_base>().HandleDamage(dmgInfo);
            }

            ApplyForce(col.GetComponent<Rigidbody>());
        }

        if (Physics.CheckSphere(transform.position, colRadius, playerMask) != false)
        {
            float dist = Vector3.Distance(transform.position, g_refs.Instance.plTrans.position);
            if (dist < dmgDist)
            {
                g_refs.Instance.plHealth.HandleDamage(dmgInfo);
            }

            ApplyForce(g_refs.Instance.plRB);
        }
    }

    private void ApplyForce(Rigidbody rb)
    {
        Vector3 dir = rb.transform.position - transform.position;
        rb.AddForce((dir + new Vector3(0, 7f, 0)) * knockbackForce, ForceMode.Impulse);
    }

    private IEnumerator HandleLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        g_refs.Instance.pool.Return(PoolType.en_exploder_explosion, transform, false);
    }
}
