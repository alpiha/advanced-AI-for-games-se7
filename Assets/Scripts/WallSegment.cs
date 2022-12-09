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
        //check to see which object we are colliding with
        switch (otherObject.tag)
        {
            case "Apple":
                
                break;
            case "Obstacle":
                Debug.Log("Ramt en mur");
                break;
            case "Player":
                
                break;
        }

    }
}
