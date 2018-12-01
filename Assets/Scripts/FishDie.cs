using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDie : MonoBehaviour {


    private Vector2 originalPos;
    private Rigidbody2D rb;

    public GameObject Speak;

	// Use this for initialization
	void Start ()
    {
        originalPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "deathLine")
        {
            fishDie();
        }
    }

    public void fishDie()
    {
        Debug.Log("you dead");

        // 播放死亡动画
        Speak.SetActive(true);
        StartCoroutine("Hit");

        // 回到初始位置
        Invoke("ResetPosition", 1.5f);

        // 禁止碰撞事件
        gameObject.GetComponent<FishJump>().CollisionEable = false;

        // 设置角色图片
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<FishJump>().jumpRenders[0];

    }

    void ResetPosition()
    {
        rb.velocity = Vector3.zero;
        transform.position = originalPos;
        Speak.SetActive(false);
        gameObject.GetComponent<FishJump>().CollisionEable = true;
    }

    // 死亡动画
    IEnumerator Hit()
    {
        //Dont show player
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Show player
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Dont show player
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Show player
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Dont show player
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Show player
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
