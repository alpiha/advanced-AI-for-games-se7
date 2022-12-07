using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GetComponent<Text>();
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }


    public void IncreaseScore()
    {
        score += 1;
    }

    public void SetScore(int score)
    {
        score = this.score;
    }

    public void ResetScore()
    {
        this.score = 0;
    }

}
