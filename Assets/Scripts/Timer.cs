using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    [Tooltip("maximum time between apple pickups")]
    public float deadtime = 60;
    private float checkpoint = 0;
    private float timer = 0;
    private TextMesh timerText;
    [Tooltip("Insert the Snake Prefab")]
    public Snake snake;
    //private Snake snakeScript;

    // Start is called before the first frame update
    void Start()
    {
     
        timerText = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localPosition = this.transform.localPosition + new Vector3(5f, 3f, 0f);
        timer += Time.deltaTime;
        timerText.text = "Time: " + timer;
        if (timer - checkpoint >= deadtime)
        {
           // Debug.Log("The Snake have not found an apple within the last: " + deadtime + " seconds");
            //snakeScript.ResetGame();
            snake.ResetGame();
        }

    }


    public void SetTimer(float time)
    {
        this.timer = time;
    }

    public float GetTimer() {
        return this.timer;
    }

    public void ResetTimer()
    {
        timer = 0.0f;
        checkpoint = 0.0f;
    }

    public float getCheckpoint() {
        return this.checkpoint;
    }

    public void setCheckpoint(float checkpoint) {
        this.checkpoint = checkpoint;
    }

}
