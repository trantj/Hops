using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {
    public static int score;        // The player's score.
    private Text TotalScore;                      // Reference to the Text component.
    public GameObject Player;
    private float starting_y;

    void Awake()
    {
        // Set up the reference.
        TotalScore = GetComponent<Text>();

        // Reset the score.
        score = 0;

        starting_y = Player.transform.position.y;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        this.GetScore();
        TotalScore.text = "Score: " + score;
    }

    private void GetScore()
    {
        int new_pos = (int)Mathf.Floor(Player.transform.position.y - starting_y);
        if (new_pos > score)
        {
            score = new_pos;
        }
    }
}
