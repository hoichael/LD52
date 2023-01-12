using UnityEngine;

public class pl_wep_tracer : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Color startColorStart;
    [SerializeField] Color endColorStart;
    Color startColorTarget, endColorTarget;

    [SerializeField] AnimationCurve fadeCurve;

    float currentFadeFactor;

    [SerializeField] float fadeSpeed;

    private void Awake()
    {
        startColorTarget = new Color(startColorStart.r, startColorStart.g, startColorStart.b, 0);
        endColorTarget = new Color(endColorStart.r, endColorStart.g, endColorStart.b, 0);

        line.enabled = false;
    }

    public void Init(Vector3 origin, Vector3 hitPoint)
    {
        line.SetPosition(0, origin);
        line.SetPosition(1, hitPoint);

        line.enabled = true;

        currentFadeFactor = 0;
    }

    private void Update()
    {
        HandleFade();

        if (currentFadeFactor == 1)
        {
            this.enabled = line.enabled = false;
        }
    }

    private void HandleFade()
    {
        currentFadeFactor = Mathf.MoveTowards(currentFadeFactor, 1, fadeSpeed * Time.deltaTime);

        //line.startColor = Color.Lerp(
        //    startColorStart,
        //    startColorTarget,
        //    fadeCurve.Evaluate(currentFadeFactor)
        //    );

        //line.endColor = Color.Lerp(
        //    endColorStart,
        //    endColorTarget,
        //    fadeCurve.Evaluate(currentFadeFactor)
        //    );

        line.startWidth = line.endWidth = Mathf.Lerp(
            0.4f,
            0f,
            fadeCurve.Evaluate(currentFadeFactor)
            );
    }
}
