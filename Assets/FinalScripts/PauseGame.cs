using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject destroyUI;

    public TextMeshProUGUI score;
    public TextMeshProUGUI health;
    public TextMeshProUGUI joyMessage;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI CongratsMessageInGame;



    public static bool NotDestroyed;
    public  bool gameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        destroyUI.SetActive(false);
        NotDestroyed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && NotDestroyed)
        {
            if (!gameIsPaused)
            {
                Pause();
            }
            else if (gameIsPaused)
            {
                Resume();
            }            
        }
        if (!NotDestroyed)
        {
            StartCoroutine(Destroyed());            
        }


    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
    }

    IEnumerator Destroyed()
    {
        yield return new WaitForSeconds(0.18f);
        endScore.text = score.text;
        destroyUI.SetActive(true);
        score.gameObject.SetActive(false);
        health.gameObject.SetActive(false);
        if (CongratsMessageInGame.gameObject.activeInHierarchy)
        {
            CongratsMessageInGame.gameObject.SetActive(false);

        }
        if (ScoreManager.alreadyPlayed)
        {
            endScore.gameObject.SetActive(false);
            joyMessage.gameObject.SetActive(true);
        }
        else
        {
            joyMessage.gameObject.SetActive(false);
            endScore.gameObject.SetActive(true);
        }
    }
      

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
