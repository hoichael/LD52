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

    //[SerializeField] GameObject enemiesContainer;
    [SerializeField] GameObject weaponsContainer;

    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro cashText;
    //int score;

    [SerializeField] TextMeshPro upperPromptText;

    [SerializeField] AudioSource musicSource;

    [SerializeField] Rigidbody plRB;
    [SerializeField] pl_cam_rot camRot;
    [SerializeField] pl_wep_manager weaponManager;
    //[SerializeField] nv_spawner enemySpawner;
    [SerializeField] wv_manager waveManager;

    [SerializeField] GameObject humanArm;
    [SerializeField] GameObject cornArmsHolder;
    [SerializeField] SkinnedMeshRenderer rendererCornRight, rendererCornLeft;

    [SerializeField] GameObject vendingMachineObj, terminalObj;

    [SerializeField] pd_session_RESET pdRest;
    [SerializeField] ui_shutter uiShutterManager;

    bool DEV_DONTSPAWN;

    private void Start()
    {
        UpdateScore(0);
        cashText.text = "CASH: " + g_refs.Instance.sessionData.cash;
        if (g_refs.Instance.sessionData.currentWaveRegular > 0)
        {
            terminalObj.SetActive(true);
            vendingMachineObj.SetActive(false);
        }
        else
        {
            vendingMachineObj.SetActive(true);
            terminalObj.SetActive(false);
        }
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

        if(Input.GetKeyDown(KeyCode.N))
        {
            DEV_DONTSPAWN = true;
        }

        // DEV STUFF
        if(Input.GetKeyDown(KeyCode.H))
        {
            StopAllCoroutines();
            HarvestTime();
            //enemiesContainer.SetActive(true);
            //weaponsContainer.SetActive(true);

            if(!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    private void IncrementSens(int mult)
    {
        settings.mouseSens += 0.1f * mult;
        settings.mouseSens = Mathf.Clamp(settings.mouseSens, 0.1f, 3);
        g_refs.Instance.sfxOneshot2D.Play(SfxType.wep_switch);
    }

    public void InitHarvestTimer()
    {
        StartCoroutine(HandleHarvestTimer());
        //musicSource.Play();
    }

    public void HarvestTime()
    {
        print("HARVEST TIME");
        monitorManager.HandleHarvestTime();
        uiManager.HandleHarvestTime();
        officeCollapser.HandleHarvestTime();

        weaponsContainer.SetActive(true);

        if (!DEV_DONTSPAWN)
        {
            //enemiesContainer.SetActive(true);
            //enemySpawner.enabled = true;
        }

        humanArm.SetActive(false);
        //cornArmsHolder.SetActive(true);
        rendererCornLeft.enabled = true;
        rendererCornRight.enabled = true;

        waveManager.InitWave();
    }

    public void UpdateScore(int scoreToAdd)
    {
        g_refs.Instance.sessionData.score += scoreToAdd;
        scoreText.text = "SCORE: " + g_refs.Instance.sessionData.score;
        //enemySpawner.OnEnemyDeath();
    }

    public void HandleDeath()
    {
        upperPromptText.text = "HARVESTED";
        plRB.isKinematic = true;
        camRot.enabled = false;
        weaponManager.DropWeapon();

        uiShutterManager.OnWaveComplete();
        StartCoroutine(HandleDeathTimer());
    }

    private IEnumerator HandleDeathTimer()
    {
        yield return new WaitForSeconds(3f);
        pdRest.ResetToDefaults();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator HandleHarvestTimer()
    {
        yield return new WaitForSeconds(harvestTimerTime);
        HarvestTime();

        //yield return new WaitForSeconds(containerEnableDelay);
        //enemiesContainer.SetActive(true);
        //weaponsContainer.SetActive(true);
    }
}
