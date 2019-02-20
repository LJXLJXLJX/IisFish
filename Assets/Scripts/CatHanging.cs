using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHanging : MonoBehaviour
{

    public float catV;
    public float animationSpeed;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Fish fishSript;
    private GameObject fish;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        sr = GetComponent<SpriteRenderer>();
        fish = GameObject.FindGameObjectWithTag("Player");
        fishSript = fish.GetComponent<Fish>();

        rb.velocity = new Vector2(-catV, 0);
        animator.speed = animationSpeed;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "leftBound")
        {
            rb.velocity = new Vector2(catV, 0);
            sr.flipX = true;
        }
        else if(collision.gameObject.name=="rightBound")
        {
            rb.velocity = new Vector2(-catV, 0);
            sr.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (fishSript.CollisionEable == true)
            {
                fishSript.fishDie();
            }
        }
    }

}
