using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_health_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshPro hpTextElement;

    [SerializeField] AudioSource hurtAudio;
    bool dead;

    private void Start()
    {
        if(g_refs.Instance.sessionData.currentWaveLooping == -1)
        {
            g_refs.Instance.sessionData.playerHP = refs.settings.maxHP;
        }

        UpdateUI();
    }

    public void HandleDamage(dmg_info dmgInfo)
    {
        if (dead) return;

        print("Damage Taken! (" + dmgInfo.dmgAmount + ")");
        //refs.state.hp -= dmgInfo.dmgAmount;
        g_refs.Instance.sessionData.playerHP -= dmgInfo.dmgAmount;
        //if(refs.state.hp <= 0)
        if (g_refs.Instance.sessionData.playerHP <= 0)
        {
            gameManager.HandleDeath();
            dead = true;
        }
        UpdateUI();

        hurtAudio.Play();
    }

    public void HandleHeal(int healAmount)
    {
        //refs.state.hp = Mathf.Clamp(refs.state.hp + healAmount, 1, refs.settings.maxHP);
        g_refs.Instance.sessionData.playerHP = Mathf.Clamp(g_refs.Instance.sessionData.playerHP + healAmount, 1, refs.settings.maxHP);
        UpdateUI();
    }

    public void UpdateUI()
    {
        //hpTextElement.text = "HEALTH: " + refs.state.hp + "/" + refs.settings.maxHP;
        //hpTextElement.text = "HEALTH: " + g_refs.Instance.sessionData.playerHP + "/" + refs.settings.maxHP;
        //hpTextElement.text = g_refs.Instance.sessionData.playerHP + "/" + refs.settings.maxHP;
        hpTextElement.text = "" + g_refs.Instance.sessionData.playerHP;
    }
}
