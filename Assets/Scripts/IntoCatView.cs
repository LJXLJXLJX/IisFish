using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoCatView : MonoBehaviour
{

    GameObject cat;

    private void Start()
    {
        cat = GameObject.Find("cat");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "fish" || collision.gameObject.name == "bucket")
        {
            Debug.Log(collision.gameObject.name);
            CatRunToTarget cr2f = cat.GetComponent<CatRunToTarget>();
            if (cr2f.readyToRun)
                cr2f.RunToTarget();
        }
    }



}
