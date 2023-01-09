using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] pl_settings settings;
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
    [SerializeField] nv_spawner enemySpawner;

    [SerializeField] GameObject humanArm;
    [SerializeField] GameObject cornArmsHolder;
    [SerializeField] SkinnedMeshRenderer rendererCornRight, rendererCornLeft;

    [SerializeField] AudioSource mouseSensAudio;

    private void Start()
    {
        UpdateScore(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.PageUp))
        {
            IncrementSens(1);
        }

        if(Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.PageDown))
        {
            IncrementSens(-1);
        }
    }

    private void IncrementSens(int mult)
    {
        mouseSensAudio.Play();
        settings.mouseSens += 0.1f * mult;
        settings.mouseSens = Mathf.Clamp(settings.mouseSens, 0.1f, 3);
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

        enemySpawner.enabled = true;

        humanArm.SetActive(false);
        //cornArmsHolder.SetActive(true);
        rendererCornLeft.enabled = true;
        rendererCornRight.enabled = true;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
        enemySpawner.OnEnemyDeath();
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
