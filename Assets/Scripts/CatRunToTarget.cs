using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRunToTarget : MonoBehaviour
{

    public float catSpeed;
    public float animSpeed;


    public bool readyToRun;
    Rigidbody2D rb;
    Vector2 originPos;
    Animation anim;
    GameObject fish;


    // Use this for initialization
    void Start()
    {
        readyToRun = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animation>();
        fish = GameObject.FindGameObjectWithTag("Player");
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunToTarget()
    {
        rb.velocity = new Vector2(-catSpeed, 0.0f);
    }

    private void resetCat()
    {
        transform.position = originPos;
        catStop();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            FishDie fd = collision.collider.gameObject.GetComponent<FishDie>();
            fd.fishDie();
            rb.velocity = Vector2.zero;
            Invoke("resetCat", 1.5f);
        }
        if(collision.collider.name=="bucket")
        {
            Invoke("catStop", 1.5f);
            readyToRun = false;
        }
    }

    private void catStop()
    {
        rb.velocity = Vector2.zero;
    }


}
