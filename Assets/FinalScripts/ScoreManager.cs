using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{


    public TextMeshProUGUI score;
    
    public TextMeshProUGUI highScoreLava;
    public TextMeshProUGUI highScoreHighway;
    public TextMeshProUGUI highScoreBeach;

    private int scoreIntLava;
    private int scoreIntHighway;
    private int scoreIntBeach;

    //public ParticleSystem[] joyCrackeres;

    public PlayerController player;
    public static bool alreadyPlayed ;

    // Start is called before the first frame update
    void Start()
    {
        alreadyPlayed = false;
        highScoreLava.text = PlayerPrefs.GetInt("HighScoreLava", 0).ToString();
        
        highScoreHighway.text = PlayerPrefs.GetInt("HighScoreHighway", 0).ToString();

        highScoreBeach.text = PlayerPrefs.GetInt("HighScoreBeach", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelLoader.LaneName == "2LaneGame")
        {
            scoreIntLava = (int)Time.timeSinceLevelLoad;
            score.text = " Score: " + scoreIntLava.ToString("0");
            if (scoreIntLava > PlayerPrefs.GetInt("HighScoreLava"))
            {
                alreadyPlayed = true;
                PlayerPrefs.SetInt("HighScoreLava", scoreIntLava);
                highScoreLava.text = "New HighScore: " + scoreIntLava.ToString("0");

            }
        }

        if (LevelLoader.LaneName == "3LaneGame")
        {
            scoreIntHighway = (int)Time.timeSinceLevelLoad;
            score.text = " Score: " + scoreIntHighway.ToString("0");
            if (scoreIntHighway > PlayerPrefs.GetInt("HighScoreHighway"))
            {
                alreadyPlayed = true;
                PlayerPrefs.SetInt("HighScoreHighway", scoreIntHighway);
                highScoreHighway.text = "New HighScore: " + scoreIntHighway.ToString("0");
            }
        }

        if (LevelLoader.LaneName == "4LaneGame")
        {
            scoreIntBeach = (int)Time.timeSinceLevelLoad;
            score.text = " Score: " + scoreIntBeach.ToString("0");
            if (scoreIntBeach > PlayerPrefs.GetInt("HighScoreBeach"))
            {
                alreadyPlayed = true;
                PlayerPrefs.SetInt("HighScoreBeach", scoreIntBeach);
                highScoreBeach.text = "New HighScore: " + scoreIntBeach.ToString("0");
            }
        }

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        throw new System.NotImplementedException();
    }



    #region For Resetting HighestScore
    public void ResetLava()
    {
        PlayerPrefs.DeleteKey("HighScoreLava");
        highScoreLava.text = "0";
    }

    public void ResetBeach()
    {
        PlayerPrefs.DeleteKey("HighScoreBeach");
        highScoreBeach.text = "0";
    }

    public void ResetHighway()
    {
        PlayerPrefs.DeleteKey("HighScoreHighway");
        highScoreHighway.text = "0";
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        highScoreLava.text = "0";
        highScoreBeach.text = "0";
        highScoreHighway.text = "0";

    }

    #endregion
}
