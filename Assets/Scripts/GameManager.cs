using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float harvestTimerTime;
    [SerializeField] ui_Manager uiManager;
    [SerializeField] nv_monitor monitorManager;
    [SerializeField] nv_office_collapse officeCollapser;

    [SerializeField] float containerEnableDelay;

    [SerializeField] GameObject enemiesContainer;
    [SerializeField] GameObject weaponsContainer;

    [SerializeField] TextMeshPro scoreText;
    int score;

    [SerializeField] TextMeshPro upperPromptText;

    [SerializeField] AudioSource musicSource;

    [SerializeField] Rigidbody plRB;
    [SerializeField] pl_cam_rot camRot;
    [SerializeField] pl_wep_manager weaponManager;

    private void Start()
    {
        UpdateScore(0);
    }

    public void InitHarvestTimer()
    {
        StartCoroutine(HandleHarvestTimer());
        musicSource.Play();
    }

    private void HarvestTime()
    {
        print("HARVEST TIME");
        monitorManager.HandleHarvestTime();
        uiManager.HandleHarvestTime();
        officeCollapser.HandleHarvestTime();

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    }

    public void HandleDeath()
    {
        upperPromptText.text = "HARVESTED";
        plRB.isKinematic = true;
        camRot.enabled = false;
        weaponManager.DropWeapon();

        StartCoroutine(HandleDeathTimer());
    }

    private IEnumerator HandleDeathTimer()
    {
        yield return new WaitForSeconds(3.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator HandleHarvestTimer()
    {
        yield return new WaitForSeconds(harvestTimerTime);
        HarvestTime();

        yield return new WaitForSeconds(containerEnableDelay);
        enemiesContainer.SetActive(true);
        weaponsContainer.SetActive(true);
    }
}
