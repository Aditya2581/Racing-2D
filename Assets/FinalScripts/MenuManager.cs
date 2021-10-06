using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

   


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HigestScore()
    {

    }

    public void Cars()
    {

    }

    

    

    public void Quit()
    {
        Application.Quit();
    }   
}
