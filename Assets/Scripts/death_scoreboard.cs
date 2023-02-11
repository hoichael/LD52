using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class death_scoreboard : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] TextMeshPro titleTextEl;
    [SerializeField] TextMeshPro scoreTextEl, highScoreTextEl_1, highScoreTextEl_2, highScoreTextEl_3, highScoreTextEl_4, highScoreTextEl_5;
    //[SerializeField] TextMeshPro highScoreTimeTextEl_1, highScoreTimeTextEl_2, highScoreTimeTextEl_3, highScoreTimeTextEl_4, highScoreTimeTextEl_5;

    [Header("Other Refs")]
    [SerializeField] pd_score scoreFileHandler;
    [SerializeField] pd_session sessionData;
    [SerializeField] pd_session_RESET dataResetter;

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

}


