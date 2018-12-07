using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadFishMove : MonoBehaviour
{

    public float vUp;
    public float vDown;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private GameObject fish;
    private Fish fishScript;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        fish = GameObject.FindGameObjectWithTag("Player");
        fishScript = fish.GetComponent<Fish>();

        rb.velocity = new Vector2(0, vUp);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "upBound")
        {
            sr.flipY = true;
            rb.velocity = new Vector2(0, -vDown);
        }
        else if (collision.gameObject.name == "lowBound")
        {
            sr.flipY = false;
            rb.velocity = new Vector2(0, vUp);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fishScript.fishDie();
        }
    }
}
