using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class pd_score : MonoBehaviour
{
    const string saveFileName = "/HIGHSCORES.HARVEST";

    public void SaveData(List<score_data> scoreList)
    {
        score_data_wrapper wrappedData = new score_data_wrapper();
        wrappedData.scores = scoreList.ToArray();
        string wrappedDataAsJSON = JsonUtility.ToJson(wrappedData);

        string path = Application.dataPath + saveFileName;

        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter streamWriter = new StreamWriter(fileStream))
        {
            streamWriter.Write(wrappedDataAsJSON);
        }
    }

    public List<score_data> GetData()
    {
        string path = Application.dataPath + saveFileName;

        if (File.Exists(path))
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string dataAsJson = streamReader.ReadToEnd();

                if (String.IsNullOrEmpty(dataAsJson) || dataAsJson == "{}")
                {
                    return null;
                }
                else
                {
                    score_data_wrapper wrappedData = JsonUtility.FromJson<score_data_wrapper>(dataAsJson);
                    return wrappedData.scores.ToList();
                }
            }
        }

        return null;
    }
}

[Serializable]
public class score_data
{
    public int score;
    public string date;

    public score_data(int score)
    {
        this.score = score;
        date = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
    }
}

[Serializable]
public class score_data_wrapper
{
    public score_data[] scores;
}