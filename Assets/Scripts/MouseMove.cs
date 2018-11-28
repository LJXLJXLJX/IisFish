using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {

    public float vUp;
    public float vDown;
    public float fishX;
    public float fishY;

    private Rigidbody2D rb;

    private bool mouseIsUp;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, vUp);
        if (vUp <= 0 || vDown <= 0)
            Debug.Log("vUp和vDown都应该是正数");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "upBound")
        {
            rb.velocity = new Vector2(0.0f, -vDown);
            mouseIsUp = false;
        }
        else if (collision.transform.name == "lowBound")
        {
            rb.velocity = new Vector2(0.0f, vUp);
            mouseIsUp = true;
        }
        else
            Debug.Log("老鼠上下触发有问题");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && mouseIsUp==true)
        {
            collision.rigidbody.velocity = new Vector2(fishX, fishY);
        }
    }
}
