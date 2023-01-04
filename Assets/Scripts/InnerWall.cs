using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerWall : MonoBehaviour
{
    public int wallLength = 5;
    public int saveSpace = 2;
    public int numberOfWalls = 3;
    public Transform segmentPrefab;
    private List<Transform> wallSegments = new List<Transform>();
    private string lastDirection = "None";
    public BoxCollider2D mapArea;
    private Vector2 location;
    // Start is called before the first frame update
    void Start()
    {
      
      
        RandomizeSpawn();
        SpawnWalls();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWalls()
    {
        wallSegments.Add(this.transform);//add back the head of the snake.
        for (int i = 0; i < numberOfWalls; i++)
        { //add segments until equal to initial size value.
            int walldirection = Random.Range(1, 4);
            switch (walldirection)
            {
                case 1: // north
                    if (lastDirection == "south")
                    {
                        i--; 
                        break; 
                    }
                    for (int e = 0; e < wallLength; e++)
                    {
                       buildWallSegment(0, e); 
                    }
                    setNewPosition();
                    lastDirection = "north";
                    break;
                case 2:  // south
                    if (lastDirection == "north")
                    {
                        i--;
                        break;
                    }
                    for (int e = 0; e < wallLength; e++)
                    {
                        buildWallSegment(0, -e);
                    }
                    setNewPosition();
                    lastDirection = "south";
                    break;
                case 3: // east 
                    if (lastDirection == "west")
                    {
                        i--;
                        break;
                    }
                    for (int e = 0; e < wallLength; e++)
                    {
                        buildWallSegment(e, 0); 
                    }
                    setNewPosition();
                    lastDirection = "east";
                    break;
                case 4: // west
                    if (lastDirection == "west")
                    {
                        i--;
                        break;
                    }
                    for (int e = 0; e < wallLength; e++)
                    {
                        buildWallSegment(-e, 0); 
                    }
                    setNewPosition();
                    lastDirection = "east";
                    break;
                default:
                    Debug.Log("Could not Build wall");
                    break;
            }   
        }
    }

    private void buildWallSegment(int j, int k)
    {
        // Vector2 mapAreaSpacex = new Vector2(mapArea.bounds.extents.x, mapArea.bounds.extents.y);
            location = new Vector2(this.transform.position.x + (j), this.transform.position.y + (k));
            //Collider2D collsionMapArea = Physics2D.OverlapBox(localtion, mapAreaSpace, LayerMask.GetMask("Obstacle"));
            if (location.x > mapArea.bounds.min.x +1 && location.x < mapArea.bounds.max.x +1 && 
                location.y > mapArea.bounds.min.y +1 && location.y < mapArea.bounds.max.y +1)            
            {
                Transform segment = Instantiate(this.segmentPrefab); 
                segment.transform.localPosition = new Vector3(location.x, location.y, 0);
                wallSegments.Add(segment);
            Debug.Log("placed Segments");
            } else
            {
                Debug.Log("Segment is out of gameMap");
            }
       
    }

    public int WallSegmentsAmount()
    {
        return wallSegments.Count;
    }

    public void RandomizeSpawn()
    {
        Bounds bounds = this.mapArea.bounds; //This is where the attribute is set. this.mapArea refers to an object you specified in unity

        //Random x and y coordinates for the apple within the bounds
        float x = Random.Range(bounds.min.x + saveSpace, bounds.max.x - saveSpace);
        float y = Random.Range(bounds.min.y + saveSpace, bounds.max.y - saveSpace);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f); //setting tje position of the apple. x and y are rounded for grid-like behavior.
    }

    private void setNewPosition()
    {
        Debug.Log("New Position is set");
        int number = wallSegments.Count;
        this.transform.position = new Vector2(wallSegments[number - 1].localPosition.x, wallSegments[number - 1].localPosition.y);
    }

    //Method that triggers open collision with another object
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //check to see if the colliding object is the "player" i.e. the snake.
        if (otherObject.tag == "Player" || otherObject.tag == "Apple")
        {
            RandomizeSpawn();
        }
    }
}
