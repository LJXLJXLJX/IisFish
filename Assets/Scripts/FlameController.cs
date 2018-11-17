using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    private Flame[] flames;

    private void Start()
    {
        flames = gameObject.GetComponentsInChildren<Flame>();
    }

    private void Update()
    {
        foreach(Flame flame in flames)
        {
            Renderer rd = flame.GetComponent<Renderer>();
            Collider2D cd = flame.GetComponent<Collider2D>();
            if (flame.getIsFlameShow())
            {
                rd.enabled = true;
                cd.isTrigger = false;
            }
            else
            {
                rd.enabled = false;
                cd.isTrigger = true;
            }
        }
    }

}
