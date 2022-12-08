using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGeneration : MonoBehaviour
{
    public BoxCollider2D mapArea; //Attribute for defining the area where the apple can spawn   
    [Tooltip("minimum space between obsticales")]
    public int safepace = 2;
    [Tooltip("maximum number of walls, SHOULD NOT be over 3")]
    public int wallAmount = 3;
    private int numberOfWalls; 
    private 
    // Start is called before the first frame update
    void Start()
    {
        if(wallAmount > 3)
        {
            Debug.Log("The maximum amount of walls is set to 3");
            wallAmount = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWall()
    {
        numberOfWalls = Random.Range(0,wallAmount);
        SetStartingPoint(getX(), getY());


    }

    private void SetStartingPoint(float x, float y)
    {
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    private float getX()
    {
        Bounds bounds = this.mapArea.bounds; //This is where the attribute is set. this.mapArea refers to an object you specified in unity
        float x = Random.Range(bounds.min.x, bounds.max.x); //Random x and y coordinates for the apple within the bounds
        return x; 
    }

    private float getY()
    {
        Bounds bounds = this.mapArea.bounds; //This is where the attribute is set. this.mapArea refers to an object you specified in unity
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return y;
    }

}
