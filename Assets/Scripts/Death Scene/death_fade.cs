using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_fade : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] float sprFadeSpeed;
    [SerializeField] AnimationCurve overlayFadeAnimCurve;

    float currentFadeFactor;
    float currentFadeTarget;

    private void Start()
    {
        StartCoroutine(HandleFadeDelay());
    }

    private IEnumerator HandleFadeDelay()
    {
        yield return new WaitForSeconds(1.1f);
        currentFadeFactor = 1;
        currentFadeTarget = 0;
    }

    public void OnSceneExit()
    {
        //currentFadeFactor = 0;
        currentFadeTarget = 1;
    }

    private void Update()
    {
        if (currentFadeFactor == currentFadeTarget) return;

        currentFadeFactor = Mathf.MoveTowards(currentFadeFactor, currentFadeTarget, (currentFadeTarget == 1 ? sprFadeSpeed * 8.4f : sprFadeSpeed) * Time.deltaTime);

        sprRenderer.color = new Color(0, 0, 0, overlayFadeAnimCurve.Evaluate(currentFadeFactor));
    }
}
