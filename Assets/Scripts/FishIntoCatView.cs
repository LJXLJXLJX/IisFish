using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIntoCatView : MonoBehaviour
{

    GameObject cat;

    private void Start()
    {
        cat = GameObject.Find("cat_0");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            CatRunToFish cr2f = cat.GetComponent<CatRunToFish>();
            cr2f.RunToFish();
        }
    }
}
