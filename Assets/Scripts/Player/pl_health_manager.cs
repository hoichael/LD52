using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_health_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshPro hpTextElement;

    bool dead;

    private void Start()
    {
        refs.state.hp = refs.settings.maxHP;
        UpdateUI();
    }

    public void HandleDamage(dmg_info dmgInfo)
    {
        if (dead) return;

        print("Damage Taken! (" + dmgInfo.dmgAmount + ")");
        refs.state.hp -= dmgInfo.dmgAmount;
        if(refs.state.hp <= 0)
        {
            gameManager.HandleDeath();
            dead = true;
        }
        UpdateUI();
    }

    public void HandleHeal(int healAmount)
    {
        refs.state.hp = Mathf.Clamp(refs.state.hp + healAmount, 1, refs.settings.maxHP);
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpTextElement.text = "HEALTH: " + refs.state.hp + "/" + refs.settings.maxHP;
    }
}
