using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITip : MonoBehaviour {

    public float animationTime;
    public float destroyTime;
    public float moveSpeed;

    public bool tipAnimation;

    void Start () {

        StartCoroutine("DestroyGo");
        StartCoroutine("TipAnimation");

        tipAnimation = true;
    }

	void Update () {

        if (tipAnimation)
        {
            //gameObject.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * moveSpeed;
        }
	}

    // 控制消失
    IEnumerator DestroyGo()
    {
        //Wait
        yield return new WaitForSeconds(destroyTime);
        //Destroy
        Destroy(gameObject);
    }

    // 控制动画
    IEnumerator TipAnimation()
    {
        //Wait
        yield return new WaitForSeconds(animationTime);
        //Destroy
        tipAnimation = false;
    }
}
