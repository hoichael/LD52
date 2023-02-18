using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_health_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshPro hpTextElement;

    [SerializeField] SpriteRenderer overlaySprRenderer;
    [SerializeField] float dmgOverlayFadeSpeed;
    float currentFadeFactor = 0;
    //[SerializeField] AudioSource hurtAudio;
    bool dead;

    private void Start()
    {
        if(g_refs.Instance.sessionData.currentWaveRegular == 0)
        {
            g_refs.Instance.sessionData.playerHP = refs.settings.maxHP;
        }

        UpdateUI();
    }

    private void Update()
    {
        if(currentFadeFactor != 0)
        {
            currentFadeFactor = Mathf.MoveTowards(currentFadeFactor, 0, dmgOverlayFadeSpeed * Time.deltaTime);
            overlaySprRenderer.color = new Color(255, 0, 0, currentFadeFactor);
        }
    }

    public void HandleDamage(dmg_info dmgInfo)
    {
        if (dead) return;
        if (currentFadeFactor != 0) return; // simple i-frames

        currentFadeFactor = 1;

        //print("Damage Taken! (" + dmgInfo.dmgAmount + ")");
        //refs.state.hp -= dmgInfo.dmgAmount;
        g_refs.Instance.sessionData.playerHP -= dmgInfo.dmgAmount;
        //if(refs.state.hp <= 0)
        if (g_refs.Instance.sessionData.playerHP <= 0)
        {
            g_refs.Instance.sessionData.playerHP = 0;
            gameManager.HandleDeath();
            dead = true;
        }
        UpdateUI();

        //hurtAudio.Play();
        g_refs.Instance.sfxOneshot2D.Play(SfxType.player_hurt);
    }

    public void HandleHeal(int healAmount)
    {
        //refs.state.hp = Mathf.Clamp(refs.state.hp + healAmount, 1, refs.settings.maxHP);
        //g_refs.Instance.sessionData.playerHP = Mathf.Clamp(g_refs.Instance.sessionData.playerHP + healAmount, 1, refs.settings.maxHP);
        g_refs.Instance.sessionData.playerHP = Mathf.Clamp(g_refs.Instance.sessionData.playerHP + healAmount, 1, g_refs.Instance.sessionData.currentPlMaxHp);
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
