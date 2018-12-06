using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBg : MonoBehaviour {

    // 背景流动速度
    public float FlowSpeed = 0.2f;

	void Start () {
		
	}
	
	void Update () {

        // 背景流动
        if (transform.position.x < 20f)
        {
            transform.Translate(Vector3.right * FlowSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(-20f, 0, 0);
        }

    }
}
