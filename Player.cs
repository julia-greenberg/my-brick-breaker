using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // audio for when player hits brick (player scores)
    public AudioClip hitPaddle;

    public AudioSource sound;

    // ball object
    public Ball ballObj;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        ballObj = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();

        // restarts level if player presses Enter
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
    }

    // Updates position of player paddle according to mouse position
    void UpdatePosition()
    {
        var mouseView = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mouseView.x < 0.05f)
        {
            mouseView.x = 0.05f;
        }
        else if (mouseView.x > .95f)
        {
            mouseView.x = 0.95f;
        }
        mouseView = Camera.main.ViewportToWorldPoint(mouseView);
        transform.position = new Vector2(mouseView.x, -4);
        //fixing rotation
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ballObj.Launched)
        {
            sound.PlayOneShot(hitPaddle);
        }
    }
}
