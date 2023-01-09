using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCounter : MonoBehaviour
{

    public InnerWall innerWall;
    private TextMesh wallCounterText;
    private bool hasCountedWalls = false;
    // Start is called before the first frame update
    void Start()
    {
        wallCounterText = this.GetComponent<TextMesh>();
        wallCounterText.text = "WallCounter: " + innerWall.GetWallAmount();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCountedWalls)
        {
            wallCounterText.text = "WallCounter: " + innerWall.GetWallAmount();
        }
    }

   
}
