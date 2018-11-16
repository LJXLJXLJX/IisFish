using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject followee;
    public float posZ;

    // Use this for initialization
    void Start()
    {
        updatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();
    }

    private void updatePosition()
    {
        Vector3 camNewPos = followee.transform.position;
        camNewPos.z = posZ;
        transform.position = camNewPos;
    }
}
