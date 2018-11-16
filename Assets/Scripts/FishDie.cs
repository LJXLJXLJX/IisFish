using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDie : MonoBehaviour {


    private Vector2 originalPos;
	// Use this for initialization
	void Start () {
        originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "deathLine")
            transform.position = originalPos;
    }
}
