using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // how many times it has been hit
    public int hitsLeft = 2;

    // broken/hit sprite
    public Sprite hit;

    // storing original position
    public Vector3 brickpos;

    // ball
    public Ball b;

    // Start is called before the first frame update
    void Start()
    {
        brickpos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ballRB.velocity = new Vector2(-ballRB.velocity.x, ballRB.velocity.y);
        if (collision.gameObject.TryGetComponent<Ball>(out b))
        {
            if (hitsLeft == 2)
            {
                hitsLeft -= 1;
                ScoreKeeper.ScorePoints(10);
            }
            else if (hitsLeft == 1)
            {
                hitsLeft -= 1;
                ScoreKeeper.ScorePoints(10);
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = brickpos;
        transform.rotation = Quaternion.identity;
        // changing appearance/sprite of brick once it has been hit once
        if (hitsLeft == 1)
        {
            GetComponent<SpriteRenderer>().sprite = hit;
        }
        // destroying brick if it has been hit twice
        if (hitsLeft == 0)
        {
            Destroy(gameObject);
        }
    }
}
