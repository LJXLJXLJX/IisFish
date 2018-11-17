using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDie : MonoBehaviour {


    private Vector2 originalPos;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start ()
    {
        originalPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "deathLine")
            fishDie();
    }

    public void fishDie()
    {
        transform.position = originalPos;
        rb.velocity = Vector3.zero;
    }
}
