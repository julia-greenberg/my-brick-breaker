using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Getting transform of player paddle
    private Transform player;

    // rigidbody of ball
    private Rigidbody2D ballRB;

    // Getting position of ball
    Vector3 ballPos;

    // if ball has been launched
    public bool Launched = false;

    // audio for when ball passes player (loses a life)
    public AudioClip lostBall;

    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        ballPos = GetComponent<Transform>().position;
        ballRB = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Launched)
        {
            // moves ball to be right above player paddle if it hasn't been launched yet
            ballPos = new Vector2(player.position.x, -3.6f);
            transform.position = ballPos;
            ballRB.velocity = new Vector2(0, 0);
        }

        // Launches ball
        if (Input.GetMouseButtonDown(0) && !Launched)
        {
            Launched = true;
            ballRB.velocity = new Vector2(10f, 10f);
        }

        var ballView = Camera.main.WorldToViewportPoint(transform.position);
        SaveBall(ballView);

        if (Launched)
        {
            Bounce();
            // if ball passes paddle - lose a life and reset ball 
            if (ballView.y < 0.05f)
            {
                LivesTracker.LoseLife(1);
                Launched = false;
                sound.PlayOneShot(lostBall);
            }
        }

    }

    // preventing ball from going off screen and thus out of bounds
    private void SaveBall(Vector3 ballView)
    {
        //var ballView = Camera.main.WorldToViewportPoint(transform.position);
        var fixPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (ballView.x <= 0)
        {
            fixPosition.x = 0;
        }
        else if (ballView.x >= 1)
        {
            fixPosition.x = 1;
        }
        else if (ballView.y >= 1)
        {
            fixPosition.y = 1;
        }
        transform.position = Camera.main.ViewportToWorldPoint(fixPosition);
    }

    // ball physics to bounce off walls and ceiling
    private void Bounce()
    {
        var ballView = Camera.main.WorldToViewportPoint(transform.position);
        if (ballView.x <= 0)
        {
            ballRB.velocity = new Vector2(-ballRB.velocity.x, ballRB.velocity.y);
        }
        else if (ballView.x >= 1)
        {
            ballRB.velocity = new Vector2(-ballRB.velocity.x, ballRB.velocity.y);
        }
        else if (ballView.y >= 1)
        {
            ballRB.velocity = new Vector2(ballRB.velocity.x, -ballRB.velocity.y);
        }

    }

    // ball rebounding off of paddle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballRB.velocity = new Vector2(ballRB.velocity.x, -ballRB.velocity.y);
    }
}
