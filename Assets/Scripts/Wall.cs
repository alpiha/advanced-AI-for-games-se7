using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public BoxCollider2D mapArea;
    private List<Transform> segments = new List<Transform>(); //List attribte for counting length of snake
    public Transform segmentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // WeNeedToBuildAWall();
    }

    public void RandomizeSpawn()
    {
        Bounds bounds = this.mapArea.bounds; //This is where the attribute is set. this.mapArea refers to an object you specified in unity

        //Random x and y coordinates for the apple within the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f); //setting tje position of the apple. x and y are rounded for grid-like behavior.
        WeNeedToBuildAWall();
    }

    private void WeNeedToBuildAWall()
    {
        
        for (int i = 1; i < 5; i++)
        { //add segments until equal to initial size value.
            Transform wallsegment = Instantiate(this.segmentPrefab);
            wallsegment.position = new Vector3(this.transform.position.x + i, this.transform.position.y, 0);
            segments.Add(wallsegment);
            Transform wallsegment2 = Instantiate(this.segmentPrefab);
            wallsegment2.position = new Vector3(this.transform.position.x + (-i), this.transform.position.y, 0);
            segments.Add(wallsegment2);




        }

    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //check to see which object we are colliding with
        switch (otherObject.tag)
        {
            case "Apple":
                RandomizeSpawn();
                break;
            case "Obstacle":
                Debug.Log("Ramt en mur");
                break;
            case "Player":
                RandomizeSpawn();
                break;
        }

    }
}
