using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreKeeper : MonoBehaviour
{
    // to store ScoreKeeper object
    public static ScoreKeeper Singleton;

    // max score for level - player has won!
    int MaxScore = 260;

    // audio for when player wins
    public AudioClip Won;

    // audio for when player hits brick (player scores)
    public AudioClip hitBrick;

    public AudioSource sound;

    // ball object so ball will be put back when player wins
    public Ball ballObj;

    // adds points to the score
    public static void ScorePoints(int points)
    {
        Singleton.ScorePointsInternal(points);
    }

    // current score
    public int Score;

    // text component displaying score
    private TMP_Text scoreDisplay;

    // Initialize Singleton and ScoreDisplay.
    void Start()
    {
        sound = GetComponent<AudioSource>();
        ballObj = FindObjectOfType<Ball>();

        Singleton = this;
        scoreDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        ScorePointsInternal(0);
    }

    // non-static private version of scorepoints
    private void ScorePointsInternal(int delta)
    {
        Score += delta;
        scoreDisplay.text = "Score: " + Score.ToString();
        if (ballObj.Launched)
        {
            sound.PlayOneShot(hitBrick);
        }       
        if (Score == MaxScore)
        {
            scoreDisplay.text = "You won!";
            ballObj.Launched = false;
            sound.PlayOneShot(Won);
        }
    }

}