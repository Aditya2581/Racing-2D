using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    public float speed;

    public float endY;
    public float startY;


    // Update is called once per frame
    void Update()
    {
        if (speed < 3)
        {
            speed += 1f;
        }
        else
        {
            speed += 0.5f;
        }

        if (speed >= 25f)
        {
            speed = 25f;
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y <= endY)
        {
            Vector2 pos = new Vector2(transform.position.x, startY);
            transform.position = pos;
        }               
    }
}
