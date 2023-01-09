using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerWall : MonoBehaviour
{
    [Tooltip("Length of each wall segments")]
    public int wallLength = 5;
    [Tooltip("The times the wall turns")]
    public int numberOfWalls = 3;
    [Tooltip("Different walls spawning on the map")]
    public int wallsOnMap = 1;
    [Tooltip("minimal Amount of Segements total")]
    public int minimumSegmentAmount = 5;
    [Tooltip("The Wall stops build this amount from MapArea")]
    public int saveSpace = 10;
    public Transform segmentPrefab;
    private List<Transform> wallSegments = new List<Transform>();
    private string lastDirection = "None";
    public BoxCollider2D mapArea;
    public List<BoxCollider2D> quadrants = new List<BoxCollider2D>();
    private List<int> quadrantsUsed = new List<int>();
    private Vector2 location;
    private Bounds spawnMap;
    // Start is called before the first frame update
    void Start()
    {

        //SpawnWalls();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
   public void CreateWalls() // there is a bug where starting Point is yeeting away from Bounds, this is a counter for that. 
   {
       
       if (GetWallAmount() <= minimumSegmentAmount)
       {
            DespawnWalls();
            SpawnWalls();
       }
   }
   

    public void DespawnWalls()
    {
        for (int i = 1; i < wallSegments.Count; i++)
        {
            Destroy(wallSegments[i].gameObject);
        }
        wallSegments.Clear(); //Clear the list of segments. (If no destroy, the segments would still exist but just not referenced)
        wallSegments.Add(this.transform); // adding new starting point back to the game.
        quadrantsUsed.Clear();
    }

    private void SpawnWalls()
    {
        for (int o = 0; o < wallsOnMap; o++)
        {
            
            if (wallsOnMap == 1)
            {
                spawnMap = this.mapArea.bounds;
            } else
            {
                spawnMap = SetFixedBounds(o);
                //Bounds bounds = SetRandomBounds();
            }
            

            RandomizeStartingPoint(spawnMap);
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
                            buildWallSegment(0, e, spawnMap);
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
                            buildWallSegment(0, -e, spawnMap);
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
                            buildWallSegment(e, 0, spawnMap);
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
                            buildWallSegment(-e, 0, spawnMap);
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

    private void buildWallSegment(int j, int k, Bounds sM)
    {
        
           // Debug.Log("Old Location:" + location);
            location = new Vector2(this.transform.position.x + (j), this.transform.position.y + (k));
           // Debug.Log("New Location:" + location);
            //Collider2D collsionMapArea = Physics2D.OverlapBox(localtion, mapAreaSpace, LayerMask.GetMask("Obstacle"));
            if (location.x > sM.min.x + saveSpace && location.x < sM.max.x - saveSpace && 
                location.y > sM.min.y + saveSpace && location.y < sM.max.y - saveSpace)            
            {
                Transform segment = Instantiate(this.segmentPrefab); 
                segment.transform.localPosition = new Vector3(location.x, location.y, 0);
                wallSegments.Add(segment);
                //Debug.Log("placed Segments");
            } else
            {
             //   Debug.Log("Segment is out of gameMap");
               // Debug.Log("starting Point: " + location);
                //Debug.Log("Bounds: " + bn);
            }  
    }
    public int GetWallAmount()
    {
        return wallSegments.Count -1; //the starting point is not destoryed, and moved to (-10.000, -10.000), and therefore it is -1
    }

    // Attempt to spawn the different walls 
    private Bounds SetRandomBounds()
    {
        Bounds bounds = this.mapArea.bounds; 
        if(wallsOnMap > 1)
        {
            int quadrant = Random.Range(0, 3);
            for (int i = 0; i <= quadrantsUsed.Count; i++)
            {
                if (i == 0 || quadrant != quadrantsUsed[i] )
                {
                    quadrantsUsed.Add(quadrant);
                    bounds = quadrants[quadrant].bounds;
                    break;
                }
            }
        } 
        return bounds;
    }
    private Bounds SetFixedBounds(int numberOfWalls)
    {
        Bounds result = quadrants[numberOfWalls].bounds;
        return result;
    }

    private void RandomizeStartingPoint(Bounds bn)
    {
        //Random x and y coordinates for the apple within the bounds minus the savespace, which the wall should not be build close to 
        //this.mapArea.bounds.SetMinMax(new Vector3(0, 0, 0), new Vector3(46, 22, 0));
        float x = Random.Range(bn.min.x, bn.max.x );
        float y = Random.Range(bn.min.y, bn.max.y );
        
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f); //setting tje position of the apple. x and y are rounded for grid-like behavior.
        //Debug.Log("Placement of starting point" + this.transform.position);
        
    }

    // else the starting point place the first segment of the new wall on last segment from the prevoius wall
    private void setNewPosition(int j, int k)
    {
        int number = wallSegments.Count;
        //Debug.Log("New Position is set, Wall Segments are" + number);
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
