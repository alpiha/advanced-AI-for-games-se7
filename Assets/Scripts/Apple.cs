using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public BoxCollider2D mapArea; //Attribute for defining the area where the apple can spawn   

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method for spawning an apple somewhere random within the map area
    public void RandomizeSpawn(){
        Bounds bounds = this.mapArea.bounds; //This is where the attribute is set. this.mapArea refers to an object you specified in unity

        //Random x and y coordinates for the apple within the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),0.0f); //setting tje position of the apple. x and y are rounded for grid-like behavior.
    }

    //Method that triggers open collision with another object
    private void OnTriggerEnter2D(Collider2D otherObject) 
    {
        //check to see if the colliding object is the "player" i.e. the snake.
        if (otherObject.tag == "Player" || otherObject.tag == "Obstacle") {
            RandomizeSpawn();
        }
    
    }

}
