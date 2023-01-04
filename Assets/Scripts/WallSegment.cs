using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSegment : MonoBehaviour
{
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
        if (otherObject.tag == "Player" || otherObject.tag == "Apple" || otherObject.tag == "Obstacle")
        {
            Destroy(this);
        }
    }

    public void DestroyMe()
    {
        Destroy(this);
    }


}
