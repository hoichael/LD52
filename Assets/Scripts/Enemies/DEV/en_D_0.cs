using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_D_0 : MonoBehaviour, I_Damagable
{
    [SerializeField] LayerMask playerHurtLayerMask;
    [SerializeField] float attackInterval;
    [SerializeField] float attackHitboxRadius;
    [SerializeField] int attackDamageAmount;

    int hp = 3;

    private void Start()
    {
        StartCoroutine(HandleAttackInterval());
    }


    private void ExecuteAttack()
    {
        if(CheckForPlayer())
        {
            //g_refs.Instance.plHealth.HandleDamage(attackDamageAmount);
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
