using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_pickup : MonoBehaviour
{
    [SerializeField] pl_state state;
    [SerializeField] pl_health_manager healthManager;
    [SerializeField] int pickupAmounttMoney, pickupAmountHealth;

    [SerializeField] TextMeshPro moneyTextEl;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Money"))
        {
            print("moneeee");
            Destroy(other.gameObject);
            state.money += pickupAmounttMoney;
            moneyTextEl.text = "CASH: " + state.money;

        }
        else if(other.CompareTag("Health"))
        {
            print("hppp");
            Destroy(other.gameObject);
            healthManager.HandleHeal(pickupAmountHealth);
        }
    }
}
