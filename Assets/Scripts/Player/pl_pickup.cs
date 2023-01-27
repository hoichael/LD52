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

    // DEV STUFF
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            //state.money = 9999999;
            g_refs.Instance.sessionData.cash = 9999999;
            moneyTextEl.text = "CASH: " + g_refs.Instance.sessionData.cash;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Money"))
        {
            //print("moneeee");
            Destroy(other.gameObject);
            g_refs.Instance.sessionData.cash += pickupAmounttMoney;
            moneyTextEl.text = "CASH: " + g_refs.Instance.sessionData.cash;

        }
        else if(other.CompareTag("Health"))
        {
            //print("hppp");
            Destroy(other.gameObject);
            healthManager.HandleHeal(pickupAmountHealth);
        }

        g_refs.Instance.sfxOneshot2D.Play(SfxType.pickup);
    }
}
