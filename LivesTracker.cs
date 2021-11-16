using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TMP_Text))]
public class LivesTracker : MonoBehaviour
{
    // to store LivesTracker objects object
    public static LivesTracker Singleton;

    // adds points to the score
    public static void LoseLife(int life)
    {
        Singleton.LoseLifeInternal(life);
    }

    // current number of lives
    public int Lives;

    // text component displaying score
    private TMP_Text livesDisplay;

    // Initialize Singleton and ScoreDisplay.
    void Start()
    {

        Singleton = this;
        livesDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        LoseLifeInternal(-3);
    }

    // non-static private version of scorepoints
    private void LoseLifeInternal(int delta)
    {
        Lives -= delta;
        livesDisplay.text = "Lives: " + Lives.ToString();
        if (Lives == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
