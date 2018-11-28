using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJump : MonoBehaviour
{
    public float maxStretch;
    public float velocityMagnitude;
    public Sprite[] jumpRenders;

    private Rigidbody2D rb;
    private bool clicked = false;
    private bool onGround = false;
    private SpriteRenderer sprender;



    private Ray2D fishToLoosePointRay;


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        fishToLoosePointRay = new Ray2D(transform.position, Vector2.zero);
        sprender = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        fishToLoosePointRay.origin = transform.position;
        if (clicked)
            updateFishRender();
    }

    private void OnMouseDown()
    {
        if (!onGround)
            return;
        clicked = true;
    }

    private void OnMouseUp()
    {
        if (!clicked)
            return;
        clicked = false;
        //Debug.Log("You is fish too hehehe...");
        sprender.sprite = jumpRenders[4];
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 loosePoint = mouseWorldPoint;
        Vector2 vecLoosePointToFish = loosePoint - transform.position;
        fishToLoosePointRay.direction = vecLoosePointToFish;
        if (vecLoosePointToFish.magnitude > maxStretch)
            loosePoint = fishToLoosePointRay.GetPoint(maxStretch);
        Vector2 v = transform.position - loosePoint;
        addVelocityOnFish(v);
    }


    private void updateFishRender()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vecMousePointToFish = mouseWorldPoint - transform.position;
        if (vecMousePointToFish.magnitude > 0 && vecMousePointToFish.magnitude < maxStretch / 3)
            sprender.sprite = jumpRenders[1];
        else if (vecMousePointToFish.magnitude >= maxStretch / 3 && vecMousePointToFish.magnitude < 2 * maxStretch / 3)
            sprender.sprite = jumpRenders[2];
        else
            sprender.sprite = jumpRenders[3];
    }


    private void addVelocityOnFish(Vector2 v)
    {
        v *= velocityMagnitude;
        rb.velocity = v;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
            sprender.sprite = jumpRenders[0];
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            onGround = false;
    }
}
