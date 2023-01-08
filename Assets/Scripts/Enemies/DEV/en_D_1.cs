using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_D_1 : MonoBehaviour, I_Damagable
{
    [SerializeField] LayerMask playerHurtLayerMask;
    [SerializeField] float attackInterval;
    [SerializeField] float attackHitboxRadius;
    [SerializeField] int attackDamageAmount;
    [SerializeField] float maxAttackDistance;
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float stoppingDistance;

    [SerializeField] float rotSlerpDamp;

    int hp = 3;

    private void Start()
    {
        if (Vector3.Distance(transform.position, g_refs.Instance.plTrans.position) < stoppingDistance) return;
        StartCoroutine(HandleAttackInterval());
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
        ChasePlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 targetVec = g_refs.Instance.plTrans.position - transform.position;
        targetVec.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(targetVec);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, rotSlerpDamp);
    }

    private void ChasePlayer()
    {
        //rb.AddForce((g_refs.Instance.plTrans.position - transform.position).normalized * moveSpeed);
        rb.AddForce(transform.forward * moveSpeed);
    }

    private void ExecuteAttack()
    {
        if (Vector3.Distance(transform.position, g_refs.Instance.plTrans.position) > maxAttackDistance) return;
        if(CheckForPlayer())
        {
            g_refs.Instance.plHealth.HandleDamage(attackDamageAmount);
        }

        StartCoroutine(HandleAttackInterval());
    }

    private bool CheckForPlayer()
    {
        return Physics.CheckSphere(
                transform.position,
                attackHitboxRadius,
                playerHurtLayerMask);
    }

    private IEnumerator HandleAttackInterval()
    {
        yield return new WaitForSeconds(attackInterval);
        ExecuteAttack();
    }

    public void HandleDamage(int amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
