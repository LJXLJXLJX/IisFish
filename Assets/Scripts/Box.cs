using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float idleV;
    public float idleInterval;
    public float sinkV;
    public float floatV;

    private Rigidbody2D rb;

    public bool isIdle;
    public bool isFloating;

    public bool boxHanging;
    private Vector2 originPos;

    // Use this for initialization
    void Start()
    {
        isIdle = false;
        Invoke("setIdleTrue", Random.Range(0.0f, 0.2f));
        isFloating = false;
        rb = GetComponent<Rigidbody2D>();
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle && !boxHanging)
            StartCoroutine(idleUpAndDown());
    }

    private IEnumerator idleUpAndDown()
    {
        boxHanging = true;
        rb.velocity = new Vector2(0, -idleV);
        yield return new WaitForSeconds(idleInterval);
        rb.velocity = new Vector2(0, idleV);
        yield return new WaitForSeconds(idleInterval);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0);
        boxHanging = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFloating = false;
            isIdle = false;
            rb.velocity = new Vector2(0, -sinkV);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("exit");
            isFloating = true;
            StartCoroutine(floatToOriginPos());
        }
    }


    private IEnumerator floatToOriginPos()
    {
        rb.velocity = new Vector2(0, floatV);
        while (true)
        {
            if (transform.position.y >= originPos.y || !isFloating)
            {
                if (!isFloating)
                    transform.position = originPos;
                break;
            }
            yield return new WaitForSeconds(0.0f);
        }
        isIdle = true;
    }


    void setIdleTrue()
    {
        isIdle = true;
    }
}
