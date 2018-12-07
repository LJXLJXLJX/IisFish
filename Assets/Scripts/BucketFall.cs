using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketFall : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer render;
    private BoxCollider2D bc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "floor")
        {
            rb.velocity = Vector2.zero;
        }
    }
}
