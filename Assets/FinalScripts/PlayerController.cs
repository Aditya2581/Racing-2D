using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public ObstacleManager spawner;

    public TextMeshProUGUI CongratsMessageInGame;

    public int numLanes;
    public float LeftMostLaneXPos, laneOffset;

    public bool playerFreez = false;

    public float Ymax;
    public float Ymin;

    public GameObject joy;
    public static GameObject[] jotPaticalEffects = new GameObject[2];

    // Stores the possible x positions in order from left to right
    [HideInInspector]
    public static float[] possiblePosX;    

    [SerializeField]
    private float speedX = 3f;
    [SerializeField]
    private float speedY = 1f;

    private Rigidbody2D rb;

    // Variables requuired for partical effects
    public GameObject collisionEffect, DestroyEffect;
        
    
    // Variable required for health
    public float FullHealth,Health,criticalHealth,healthDecreament;
    public TextMeshProUGUI healthText;

    private Vector2 targetPos;
    [HideInInspector]
    public static int currentPosX;
    

    /// <summary>
    /// used to get the rigidbody component of player
    /// </summary>
    void Start()
    {
        jotPaticalEffects[0] = joy;
        jotPaticalEffects[1] = joy;

        Time.timeScale = 1;
        possiblePosX = new float[numLanes];
        for (int i = 0; i < numLanes; i++)
        {
            possiblePosX[i] = LeftMostLaneXPos + i * laneOffset;
        }
        rb = GetComponent<Rigidbody2D>();
        targetPos = transform.position;
        currentPosX = 0;
        AudioListener.pause = false;
        joyParticalEffectisPlaying = false;
    }

    #region Player Movement
    /// <summary>
    /// Manages the player input and smoke at critical health
    /// </summary>
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.RightArrow) && currentPosX != possiblePosX.Length - 1)
        {            
            currentPosX += 1;
            targetPos.x = possiblePosX[currentPosX];
            
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) && currentPosX != 0)
        {            
            currentPosX -= 1;
            targetPos.x = possiblePosX[currentPosX];            
        }
        

        if (Input.GetKey(KeyCode.UpArrow))
        {            
            targetPos.y = transform.position.y + speedY;
            if (targetPos.y > Ymax)
            {
                targetPos.y = Ymax;
            }
        
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            targetPos.y = transform.position.y - speedY;
            if (targetPos.y < Ymin)
            {
                targetPos.y = Ymin;
            }           
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speedX * Time.deltaTime);

        if (Health < criticalHealth && Health > 0 && !lowHeath)
        {
            StartSmokeOnLowHealth();           
        }

        healthText.text = "Health : "+ Health.ToString("0.0");

        NewHighestScore();
    }
    #endregion


    #region Low Health Ckecking and Effect

    private bool lowHeath = false;    

    private void StartSmokeOnLowHealth()
    {
        lowHeath = true;
        StartCoroutine(LowHealthSmoke());
    }

    public GameObject[] smoke;

    /// <summary>
    /// Used for producing effect of continuous smoke and on low health
    /// </summary>
    /// <returns>hold time for spawninng other smoke effect</returns>
    IEnumerator LowHealthSmoke()
    {
        yield return new WaitForSeconds(1f);
        smoke[0].SetActive(true);
        yield return new WaitForSeconds(0.75f);
        smoke[1].SetActive(false);
        yield return new WaitForSeconds(1f);
        smoke[1].SetActive(true);
        yield return new WaitForSeconds(0.75f);
        smoke[0].SetActive(false);
        
        if (Health > 0 && Health <= criticalHealth)
        {
            StartCoroutine(LowHealthSmoke());            
        }
        else if (Health > criticalHealth)
        {
            smoke[1].SetActive(false);
            lowHeath = false;
        }
    }
    #endregion


    #region Collision Detection
    /// <summary>
    /// Determines if player collided with normal car or special car
    /// </summary>
    /// <param name="collision"> used to get the tag of collided object </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Collision with normal Cars
        if (collision.gameObject.CompareTag("Obstacle") && Health > 0.5f)
        {
            Collision();
            Health -= healthDecreament;            
        }
        else if(collision.gameObject.CompareTag("Obstacle") && Health == 0.5f)
        {
            GameOver();
        }
        #endregion

        #region Collision with Ambulance
        else if (collision.gameObject.CompareTag("Ambulance"))
        {
            spawner.ObstaclesInScene.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            Health = FullHealth;
        }
        #endregion

        #region Collision with Police
        else if (collision.gameObject.CompareTag("Police"))
        {
            GameOver();
        }
        #endregion
    }
    #endregion

    private void Collision()
    {
        GameObject smallCollision = Instantiate(collisionEffect, transform) as GameObject;
        Vector2 tempPosition = smallCollision.transform.position;
        smallCollision.transform.position = tempPosition + new Vector2(0, 0.45f);        
    }

    private void GameOver()
    {
        Health = 0f;
        PauseGame.NotDestroyed = false;
        DestroyEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Player Death");
        playerFreez = true;
        Time.timeScale = 0.05f;
        StartCoroutine(StopAllCars());
        
    }

    IEnumerator StopAllCars()
    {
        yield return new WaitForSeconds(0.2f);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        
    }

    private bool joyParticalEffectisPlaying;

    public void NewHighestScore()
    {        
        if (ScoreManager.alreadyPlayed && !joyParticalEffectisPlaying)
        {
            StartCoroutine(CongratsInGame());
            Instantiate(jotPaticalEffects[0], new Vector3(-4.29f, 1.46f, 0), Quaternion.identity);
            Instantiate(jotPaticalEffects[1], new Vector3(4.29f, 1.46f, 0), Quaternion.identity);
            joyParticalEffectisPlaying = true;
        }
    }

    IEnumerator CongratsInGame()
    {
        CongratsMessageInGame.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        CongratsMessageInGame.gameObject.SetActive(false);
    }
}