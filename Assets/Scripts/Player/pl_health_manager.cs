using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_health_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] DevManager devManager;
    [SerializeField] TextMeshPro hpTextElement;

    private void Start()
    {
        refs.state.hp = refs.settings.maxHP;
        UpdateUI();
    }

    public void HandleDamage(dmg_info dmgInfo)
    {
        print("Damage Taken! (" + dmgInfo.dmgAmount + ")");
        refs.state.hp -= dmgInfo.dmgAmount;
        if(refs.state.hp <= 0)
        {
            devManager.Reload();
        }
        UpdateUI();
    }

    public void HandleHeal(int healAmount)
    {
        refs.state.hp = Mathf.Clamp(refs.state.hp + healAmount, 1, refs.settings.maxHP);
        UpdateUI();
    }

    private void UpdateUI()
    {
        hpTextElement.text = "Health: " + refs.state.hp;
    }
}
