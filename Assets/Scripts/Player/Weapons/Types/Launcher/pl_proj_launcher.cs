using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_proj_launcher : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    //[SerializeField] Collider col;
    [SerializeField] GameObject modelAndCol;

    [SerializeField] float moveSpeed;
    [SerializeField] float gravForceMax;
    [SerializeField] float gravGrowFactor;
    [SerializeField] TrailRenderer trail;

    [SerializeField] Transform modelContainer;

    [SerializeField] float lifeTime;

    float currentGravForce;

    private void OnEnable()
    {
        trail.emitting = true;
        currentGravForce = 0;
        rb.isKinematic = false;
        //col.enabled = true;
        modelAndCol.SetActive(true);

        gameObject.SetActive(true);
        StartCoroutine(HandleLifetime());

        // hmmmmmm
        rb.velocity = transform.forward * 9;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
        rb.AddForce(Vector3.down * currentGravForce, ForceMode.Acceleration);

        currentGravForce = Mathf.MoveTowards(currentGravForce, gravForceMax, gravGrowFactor);
        //print(currentGravForce);

        modelContainer.LookAt(transform.position + rb.velocity.normalized);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("COL!");
        //gameObject.SetActive(false);
        Explode();
    }

    private void Explode()
    {
        g_refs.Instance.pool.Dispatch(PoolType.launcher_explosion, transform.position, Quaternion.identity);

        StopAllCoroutines();
        rb.rotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;

        rb.isKinematic = true;
        //col.enabled = false;
        modelAndCol.SetActive(false);

        StartCoroutine(DelayedReturnToPool());
    }

    private IEnumerator HandleLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }

    private IEnumerator DelayedReturnToPool() // keep trail visible after impact
    {
        yield return new WaitForSeconds(3f);
        trail.emitting = false;
        g_refs.Instance.pool.Return(PoolType.proj_launcher, rb.transform, false);
    }
}
