using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class death_score : MonoBehaviour
{
    [SerializeField] TextMeshPro titleTextEl, scoreTextEl, highScoreTextEl_1, highScoreTextEl_2, highScoreTextEl_3, highScoreTextEl_4, highScoreTextEl_5;
    [SerializeField] TextMeshPro highScoreTimeTextEl_1, highScoreTimeTextEl_2, highScoreTimeTextEl_3, highScoreTimeTextEl_4, highScoreTimeTextEl_5;

    private void Awake()
    {
        
    }
}

public class score_data
{
    public int score;
    public DateTime date;

    public score_data(int score)
    {
        this.score = score;
        date = DateTime.Now;
    }
}
