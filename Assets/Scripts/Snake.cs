using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right; //Attribute containing which direction snake is moving. (default right)
    private List<Transform> segments = new List<Transform>(); //List attribte for counting length of snake
    public Transform segmentPrefab;
    public int initialSize = 3;
    public Timer timerScript;
    public Score scoreScript;
    private Apple appleScript;
    public SpriteRenderer background;
    private bool initilizedGame = false; 

    // Start is called before the first frame update
    void Start()
    {
       // scoreScript = GameObject.Find("ScoreText").GetComponent<Score>();
        //timerScript = GameObject.Find("TimerText").GetComponent<Timer>();
        appleScript = GameObject.Find("Apple").GetComponent<Apple>();
        //Look at the ResetGame method for more info
        ResetGame();

    }

    // Update is called once per frame
    void Update()
    { 
        //Code for changing direction the snake move with WASD
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            if(direction != Vector2.down) {
                direction = Vector2.up;
            }
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            if(direction != Vector2.up) {
                direction = Vector2.down;
            }
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            if(direction != Vector2.right) {
                direction = Vector2.left;
            }
        }else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if(direction != Vector2.left) {
                direction = Vector2.right;
            }
        }
    }

    //FixedUpdate is a method that is called at a fixed interval, which can be configure in unity. (Edit->Project settings->Time->Fixed Time step = 0.06)
    private void FixedUpdate()
    {
        //Loop through list of segments IN REVERSE order.
        for(int i = segments.Count -1; i > 0; i--) {
            segments[i].localPosition = segments[i-1].position; //Starting from the tail, the segments position is set to the position of the segment ahead of it.
        }
        
        //Lastly, update the positon of the head of the snake. Postion is rounded to move in grid-like manner
        this.transform.localPosition = new Vector3(
            Mathf.Round(this.transform.localPosition.x) + direction.x,
            Mathf.Round(this.transform.localPosition.y) + direction.y,
            0.0f //This is 0 bc Snake is a 2D game and the Z axis is not needed
        );
    }

    //Method for adding a segment to the segmet list by instantiating a new prefab from the asset folder.
    private void Grow() 
    {
        scoreScript.IncreaseScore();
        timerScript.setCheckpoint(timerScript.GetTimer());
        Transform segment = Instantiate(this.segmentPrefab); //Instantiate a new segment using prefab from asset folder
        segment.localPosition = segments[segments.Count - 1].localPosition;//Add the segment to the tail of the snake by getting the postion of the last segment in the list

        segments.Add(segment);
    }

    //Method for putting state of the game back to original state
    public void ResetGame()
    {
        if (!initilizedGame)
        {
            initilizedGame = true;
        } else
        {
            Debug.Log("GAME HAS BEEN RESETED, You died after " + timerScript.GetTimer() + " seconds and the last score was: " + scoreScript.GetScore());
        }
        Time.timeScale = 1;
        //Loop through Segment list completely destroy the segments
        for (int i=1; i<segments.Count; i++ ) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear(); //Clear the list of segments. (If no destroy, the segments would still exist but just not referenced)
        
        segments.Add(this.transform);//add back the head of the snake.
        for(int i = 1; i<initialSize; i++) { //add segments until equal to initial size value.
            segments.Add(Instantiate(this.segmentPrefab));
        }

       // this.transform.position = Vector3.zero; //reset position back to the middle. 
        this.transform.localPosition = new Vector3(0, 0, 0);
        // reset game attibutes
        
        appleScript.RandomizeSpawn();
        scoreScript.ResetScore();
        timerScript.ResetTimer();
    }
    
    
    //Method that triggers open collision with another object
    private void OnTriggerEnter2D(Collider2D otherObject) 
    {
        //check to see which object we are colliding with
        switch(otherObject.tag) {
            case "Apple":
                Grow();
                StartCoroutine(ChangeBackgroundGreen());
                break;
            case "Obstacle":
                Debug.Log(gameObject + " died to: " + otherObject);
                ResetGame();
                StartCoroutine(ChangeBackgroundRed());
                break;
        }

        IEnumerator ChangeBackgroundRed()
        {
            background.color = new Color(1, 0, 0, 0.5f);
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(0.125f);
            background.color = new Color(0, 0, 0, 1);
        }

        IEnumerator ChangeBackgroundGreen()
        {
            background.color = new Color(0, 1, 0, 0.5f);
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(0.125f);
            background.color = new Color(0, 0, 0, 1);
        }


    }
}
