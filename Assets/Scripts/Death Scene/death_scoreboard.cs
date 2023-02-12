using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class death_scoreboard : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] TextMeshPro titleTextEl;
    [SerializeField] TextMeshPro newHighscoreTextEl, scoreTextEl, highScoreTextEl_1, highScoreTextEl_2, highScoreTextEl_3, highScoreTextEl_4, highScoreTextEl_5;
    //[SerializeField] TextMeshPro highScoreTimeTextEl_1, highScoreTimeTextEl_2, highScoreTimeTextEl_3, highScoreTimeTextEl_4, highScoreTimeTextEl_5;

    [Header("Other Refs")]
    [SerializeField] pd_score scoreFileHandler;
    [SerializeField] pd_session sessionData;
    [SerializeField] pd_session_RESET dataResetter;


    [Header("Values")]
    [SerializeField] float textPulseAnimSpeed;

    float currentTextPulseAnimFactor;

    List<score_data> scoreDataList;

    private void Awake()
    {
        scoreDataList = new List<score_data>();
        scoreDataList.Add(new score_data(42));
        scoreDataList.Add(new score_data(333));

        scoreFileHandler.SaveData(scoreDataList);

        List<score_data> newScoreData = scoreFileHandler.GetData();
        print(newScoreData.Count);

        print(newScoreData[1].score);







        dataResetter.ResetToDefaults();
    }

    private void Update()
    {
        HandleHighscoreTextScale();
    }

    private void HandleHighscoreTextScale()
    {
        currentTextPulseAnimFactor = Mathf.MoveTowards(currentTextPulseAnimFactor, 1, textPulseAnimSpeed * Time.deltaTime);

        newHighscoreTextEl.transform.localScale = Vector3.Lerp(
            Vector3.one,
            new Vector3(1.6f, 1.6f, 1.6f),
            Mathf.PingPong(currentTextPulseAnimFactor, 0.5f)
            );

        if (currentTextPulseAnimFactor == 1) currentTextPulseAnimFactor = 0;
    }

}


