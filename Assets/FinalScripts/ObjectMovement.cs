using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    public float speedofObject;
    public float FirstmaxSpeed;

    public float baseSpeed;
    
    void Start()
    {
        if (speedofObject < FirstmaxSpeed)
        {
            speedofObject = baseSpeed + 0.5f * Time.timeSinceLevelLoad;
        }
        else if (speedofObject >= FirstmaxSpeed)
        {
            
            speedofObject = baseSpeed + 0.05f * Time.timeSinceLevelLoad;
        }        
    }

    
    void Update()
    {        
        if (!AudioListener.pause && PauseGame.NotDestroyed)
        {
            transform.Translate(Vector2.down * speedofObject * Time.deltaTime);            
        }        
    }

    
}
