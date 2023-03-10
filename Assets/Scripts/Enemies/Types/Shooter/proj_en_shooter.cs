using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proj_en_shooter : MonoBehaviour
{
    [SerializeField] float startScale, targetScale;
    [SerializeField] float growSpeed;
    [SerializeField] dmg_info dmgInfo;
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] TrailRenderer trail;


    private void OnEnable()
    {
        transform.localScale = new Vector3(startScale, startScale, startScale);
        trail.widthMultiplier = startScale;
        rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(targetScale, targetScale, targetScale), growSpeed);
        trail.widthMultiplier = Mathf.MoveTowards(trail.widthMultiplier, targetScale, growSpeed * 0.4f);
        rb.velocity = transform.forward * moveSpeed; 
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.CompareTag("Player"))
        {
            g_refs.Instance.plHealth.HandleDamage(dmgInfo);
        }

        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.Euler(Vector3.zero);
        rb.isKinematic = true;
        g_refs.Instance.pool.Return(PoolType.proj_en_shooter, transform, false);
    }
}
