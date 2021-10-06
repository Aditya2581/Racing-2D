using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator crossFade;

    public static string LaneName;

    private float transitionTime = 1f;

    public void Play()
    {
        StartCoroutine(LoadGame(LaneName));
    }

    public void TwoLane()
    {

        LaneName = "2LaneGame";
        StartCoroutine(LoadGame(LaneName));
    }

    public void ThreeLane()
    {
        LaneName = "3LaneGame";
        StartCoroutine(LoadGame(LaneName));
    }

    public void FourLane()
    {

        LaneName = "4LaneGame";
        StartCoroutine(LoadGame(LaneName));
    }

    public void ReturnToMainMenu()
    {
        LaneName = "Menu";
        SceneManager.LoadScene(LaneName);        
    }

    IEnumerator LoadGame(string Lane)
    {
        crossFade.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(Lane);
    }
}
