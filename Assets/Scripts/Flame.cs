using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{


    public float time;
    public float interval;

    private FishDie fd;
    private bool isFlameShow;


    // Use this for initialization
    void Start()
    {
        isFlameShow = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > interval)
        {
            if (isFlameShow)
                flameFade();
            else
                flameShow();
            time = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject collisionObj;
        if (collision.collider.tag == "Player")
        {
            collisionObj = collision.collider.gameObject;
            fd = collisionObj.GetComponent<FishDie>();
            fd.fishDie();
        }
    }


    private void flameFade()
    {
        isFlameShow = false;
    }

    private void flameShow()
    {
        isFlameShow = true;
    }

    public bool getIsFlameShow()
    {
        return isFlameShow;
    }
}
