using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_shutter : MonoBehaviour
{
    [Header("Shutter")]
    [SerializeField] float shutterAnimSpeed;
    [SerializeField] Transform shutterTop, shutterBottom;
    [SerializeField] AnimationCurve shutterAnimCurve;
    [SerializeField] float shutterYInit, shutterYHidden;

    [Header("Overlay")]
    [SerializeField] SpriteRenderer fullscreenSpr;
    [SerializeField] float sprFadeSpeed;
    [SerializeField] AnimationCurve overlayFadeAnimCurve;


    [Header("Pixelation")]
    [SerializeField] float pixelationChangeInterval;
    [SerializeField] RenderTexture[] rtArr;
    [SerializeField] Material[] matArr;

    public float currentSprFadeFactor = 0;
    float currentSprFadeTarget = 0;
    float currentShutterFactor = 1;
    float currentShutterTarget = 1;

    private void Start()
    {
        if (g_refs.Instance.sessionData.currentWaveRegular == 0)
        {
            fullscreenSpr.gameObject.SetActive(true);
            currentSprFadeFactor = 1;
            currentSprFadeTarget = 0;
        }
        else
        {
            shutterTop.gameObject.SetActive(true);
            shutterBottom.gameObject.SetActive(true);
            //StartCoroutine(OnSceneEnter());
            currentShutterFactor = 0;
            currentShutterTarget = 1;
        }
    }

    private void Update()
    {
        if (currentSprFadeFactor != currentSprFadeTarget) HandleSpriteFade();
        if (currentShutterFactor != currentShutterTarget) HandleShutterAnim();
    }

    private void HandleSpriteFade()
    {
        currentSprFadeFactor = Mathf.MoveTowards(currentSprFadeFactor, currentSprFadeTarget, (currentSprFadeTarget == 1 ? sprFadeSpeed * 2.8f : sprFadeSpeed) * Time.deltaTime);

        fullscreenSpr.color = new Color(0, 0, 0, overlayFadeAnimCurve.Evaluate(currentSprFadeFactor));
    }

    private void HandleShutterAnim()
    {
        currentShutterFactor = Mathf.MoveTowards(currentShutterFactor, currentShutterTarget, shutterAnimSpeed * Time.deltaTime);

        shutterTop.localPosition = Vector3.Lerp(
            new Vector3(0, shutterYInit, 0),
            new Vector3(0, shutterYHidden, 0),
            shutterAnimCurve.Evaluate(currentShutterFactor)
            );

        shutterBottom.localPosition = Vector3.Lerp(
            new Vector3(0, -shutterYInit, 0),
            new Vector3(0, -shutterYHidden, 0),
            shutterAnimCurve.Evaluate(currentShutterFactor)
            );
    }

    public void OnDeath()
    {
        currentSprFadeFactor = 0;
        currentSprFadeTarget = 1;
    }

    public void OnWaveComplete()
    {
        shutterTop.gameObject.SetActive(true);
        shutterBottom.gameObject.SetActive(true);

        // shouldnt make a difference but prevents weird flash
        shutterTop.localPosition = new Vector3(0, shutterYHidden, 0);
        shutterBottom.localPosition = new Vector3(0, -shutterYHidden, 0);

        currentShutterTarget = 0;
    }
}
