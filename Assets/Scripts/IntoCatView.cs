using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIntoCatView : MonoBehaviour
{

    GameObject cat;

    private void Start()
    {
        cat = GameObject.Find("cat");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "fish" || collision.gameObject.name == "buket")
        {
            CatRunToTarget cr2f = cat.GetComponent<CatRunToTarget>();
            cr2f.RunToTarget();
        }
    }



}
