using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerWall : MonoBehaviour
{
    [Tooltip("Length of each wall segments")]
    public int wallLength = 5;
    [Tooltip("The Wall stops build this amount from MapArea")]
    public int saveSpace = 2;
    [Tooltip("The times the wall turns")]
    public int numberOfWalls = 3;
    [Tooltip("Different walls spawning on the map")]
    public int wallsOnMap = 1;
    public Transform segmentPrefab;
    private List<Transform> wallSegments = new List<Transform>();
    private List<Transform> startingPoints = new List<Transform>();
    private string lastDirection = "None";
    public BoxCollider2D mapArea;
    private Vector2 location;
    // Start is called before the first frame update
    void Start()
    {

        //SpawnWalls();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DespawnWalls()
    {
        for (int i = 1; i < wallSegments.Count; i++)
        {
            Destroy(wallSegments[i].gameObject);
        }
        wallSegments.Clear(); //Clear the list of segments. (If no destroy, the segments would still exist but just not referenced)
        wallSegments.Add(this.transform); // adding new starting point back to the game.
    }

    public void SpawnWalls()
    {
        for (int o = 0; o < wallsOnMap; o++)
        {
            RandomizeStartingPoint();
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
                        setNewPosition(0, 1);
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
                        setNewPosition(0, -1);
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
                        setNewPosition(1, 0);
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
                        setNewPosition(-1, 0);
                        lastDirection = "east";
                        break;
                    default:
                        Debug.Log("Could not Build wall");
                        break;
                }
            }
        }
        this.transform.localPosition = new Vector2(-10_000, -10_000);

    }

    private void buildWallSegment(int j, int k)
    {
        // Vector2 mapAreaSpacex = new Vector2(mapArea.bounds.extents.x, mapArea.bounds.extents.y);
            location = new Vector2(this.transform.position.x + (j), this.transform.position.y + (k));
            //Collider2D collsionMapArea = Physics2D.OverlapBox(localtion, mapAreaSpace, LayerMask.GetMask("Obstacle"));
            if (location.x > mapArea.bounds.min.x + saveSpace && location.x < mapArea.bounds.max.x - saveSpace && 
                location.y > mapArea.bounds.min.y + saveSpace && location.y < mapArea.bounds.max.y - saveSpace)            
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
    public int GetWallAmount()
    {
        return wallSegments.Count -1;
    }

    private void RandomizeStartingPoint()
    {
        Bounds bounds = this.mapArea.bounds; //This is where the attribute is set. this.mapArea refers to an object you specified in unity

        //Random x and y coordinates for the apple within the bounds minus the savespace, which the wall should not be build close to 
        float x = Random.Range(bounds.min.x, bounds.max.x );
        float y = Random.Range(bounds.min.y, bounds.max.y );

        /*
        if (startingPoints.Count >=1) // ensure that the starting point can't spawn within a radius of wallLength * numberOfWalls NOT WORKING
        {
            for (int i = 0; i > startingPoints.Count - 1; i++)
            {
                if (startingPoints[i].transform.localPosition.x - (x) < wallLength * numberOfWalls && startingPoints[i].transform.localPosition.y - (y) < wallLength * numberOfWalls ||
                    startingPoints[i].transform.localPosition.x + (x) > wallLength * numberOfWalls && startingPoints[i].transform.localPosition.y + (y) > wallLength * numberOfWalls)
                {
                    RandomizeStartingPoint();
                }
            }
        }
        */
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f); //setting tje position of the apple. x and y are rounded for grid-like behavior.
        Transform startPoint = this.transform;
        startingPoints.Add(startPoint);
    }

    private void setNewPosition(int j, int k)
    {
        int number = wallSegments.Count;
        Debug.Log("New Position is set, Wall Segments are" + number);
        this.transform.position = new Vector2(wallSegments[number-1].localPosition.x + (j), wallSegments[number-1].localPosition.y +(k));
    }

    //Method that triggers open collision with another object
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //check to see if the colliding object is the "player" i.e. the snake.
        if (otherObject.tag == "Player" || otherObject.tag == "Apple")
        {
            SpawnWalls();
        }
    }
}
