using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class death_scoreboard : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] TextMeshPro runScoreTitleTextEl;
    [SerializeField] TextMeshPro newHighscoreTextEl;
    [SerializeField] TextMeshPro[] highscoreTextElValuesArr, highscoreTextElDatesArr;

    [Header("Other Refs")]
    [SerializeField] pd_score scoreFileHandler;
    [SerializeField] pd_session sessionData;
    [SerializeField] pd_session_RESET dataResetter;
    [SerializeField] death_fade overlayFader;

    [Header("Values")]
    [SerializeField] float textPulseAnimSpeed;

    float currentTextPulseAnimFactor;
    bool canExitScene;

    List<score_data> scoreDataList;

    private void Start()
    {
        Cursor.visible = false;

        runScoreTitleTextEl.text = "FINAL SCORE: " + sessionData.score.ToString();

        scoreDataList = scoreFileHandler.GetData();
        if(scoreDataList == null)
        {
            scoreDataList = new List<score_data>();
            newHighscoreTextEl.gameObject.SetActive(true);
        }
        else
        {
            SortList();
            if(sessionData.score > scoreDataList[0].score)
            {
                newHighscoreTextEl.gameObject.SetActive(true);
            }
        }

        scoreDataList.Add(new score_data(sessionData.score));
        SortList();
        scoreFileHandler.SaveData(scoreDataList);

        SetTextElements();

        //dataResetter.ResetToDefaults();
    }

    private void SetTextElements()
    {
        for(int i = 0; i < 5; i++)
        {
            if(scoreDataList[i] != null)
            {
                highscoreTextElValuesArr[i].text = scoreDataList[i].score.ToString();
                highscoreTextElDatesArr[i].text = scoreDataList[i].date;
            }
            else
            {
                highscoreTextElValuesArr[i].text = "N/A";
                highscoreTextElDatesArr[i].text = "N/A";
            }
        }
    }

    private void SortList()
    {
        score_data[] dataAsArr = scoreDataList.ToArray();
        IEnumerable<score_data> dataAsSortedArr = dataAsArr.OrderByDescending(entry => entry.score);
        scoreDataList = dataAsSortedArr.ToList();
    }

    private void Update()
    {
        HandleHighscoreTextScale();

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
        {
            overlayFader.OnSceneExit();
            StartCoroutine(HandleDelayedSceneSwitch());
        }
    }

    private IEnumerator HandleDelayedSceneSwitch()
    {
        yield return new WaitForSeconds(2.45f);
        dataResetter.ResetToDefaults();
        SceneManager.LoadScene(0);
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


