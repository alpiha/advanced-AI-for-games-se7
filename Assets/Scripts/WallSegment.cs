using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSegment : MonoBehaviour
{
    private Apple apple;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //check to see if the colliding object is the "player" i.e. the snake.
        if (otherObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
        else if (otherObject.tag == "Apple")
        {
            apple.RandomizeSpawn();
        }
    }

    public void DestroyMe()
    {
        Destroy(this);
    }


}
