using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_fade : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] float sprFadeSpeed;
    [SerializeField] AnimationCurve overlayFadeAnimCurve;

    float currentFadeFactor;

    private void Start()
    {
        StartCoroutine(HandleFadeDelay());
    }

    private IEnumerator HandleFadeDelay()
    {
        yield return new WaitForSeconds(0.85f);
        currentFadeFactor = 1;
    }

    private void Update()
    {
        if (currentFadeFactor == 0) return;

        currentFadeFactor = Mathf.MoveTowards(currentFadeFactor, 0, sprFadeSpeed * Time.deltaTime);

        sprRenderer.color = new Color(0, 0, 0, overlayFadeAnimCurve.Evaluate(currentFadeFactor));
    }
}
