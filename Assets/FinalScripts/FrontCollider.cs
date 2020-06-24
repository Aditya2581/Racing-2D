using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCollider : MonoBehaviour
{
    public GameObject[] roads;
    public AudioSource breakingSound;

    public PlayerController playerScript;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.Health > 0.5 && collision.CompareTag("Obstacle"))
        {
            breakingSound.Play();
            for (int i = 0; i < roads.Length; i++)
            {
                roads[i].GetComponent<RepeatingBG>().speed = 0f;
            }
        }        
    }
}
