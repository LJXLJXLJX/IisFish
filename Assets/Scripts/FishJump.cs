using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishJump : MonoBehaviour
{
    public float maxStretch;
    public float velocityMagnitude;
    public Sprite[] jumpRenders;

    public GameObject Circle;
    public GameObject Dot;
    public GameObject Arrow;

    public GameObject Tip;

    public bool CollisionEable;

    private Rigidbody2D rb;
    [SerializeField]
    private bool clicked = false;
    [SerializeField]
    private bool onGround = false;
    private SpriteRenderer sprender;



    private Ray2D fishToLoosePointRay;


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        fishToLoosePointRay = new Ray2D(transform.position, Vector2.zero);
        sprender = gameObject.GetComponent<SpriteRenderer>();

        CollisionEable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            updateFishRender();
            updateCircle();
        }
    }

    private void OnMouseDown()
    {
        if (!onGround)
            return;
        clicked = true;

        Circle.SetActive(true);
        Dot.SetActive(true);
        Arrow.SetActive(true);

        Tip.SetActive(false);
    }

    private void OnMouseUp()
    {
        if (!clicked)
            return;
        clicked = false;
        Circle.SetActive(false);
        Dot.SetActive(false);
        Arrow.SetActive(false);

        if (!CollisionEable)
            return;
        //Debug.Log("You is fish too hehehe...");
        sprender.sprite = jumpRenders[4];
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 loosePoint = mouseWorldPoint;
        Vector2 vecLoosePointToFish = loosePoint - transform.position;
        fishToLoosePointRay.origin = transform.position;
        fishToLoosePointRay.direction = vecLoosePointToFish;
        if (vecLoosePointToFish.magnitude > maxStretch)
            loosePoint = fishToLoosePointRay.GetPoint(maxStretch);
        Vector2 v = transform.position - loosePoint;
        addVelocityOnFish(v);

    }


    private void updateFishRender()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vecMousePointToFish = mouseWorldPoint - transform.position;
        if (vecMousePointToFish.magnitude > 0 && vecMousePointToFish.magnitude < maxStretch / 3)
            sprender.sprite = jumpRenders[1];
        else if (vecMousePointToFish.magnitude >= maxStretch / 3 && vecMousePointToFish.magnitude < 2 * maxStretch / 3)
            sprender.sprite = jumpRenders[2];
        else
            sprender.sprite = jumpRenders[3];
    }

    // 显示操作界面
    private void updateCircle()
    {
        // 鼠标位置
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 鼠标位置-鱼的位置
        Vector2 vecMousePointToFish = mouseWorldPoint - transform.position;
        // 方向角度
        float angle = Mathf.Atan2(vecMousePointToFish.y, vecMousePointToFish.x);

        // 圆点位置调整
        // 如果圆点在圆的范围内
        if (vecMousePointToFish.magnitude <= 3.5f)
        {
            // 圆点位置为鼠标位置
            Dot.transform.position = new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, 0);
        }
        else
        {
            // 圆点位置在圆的边界
            Vector3 DotPosition = new Vector3(3.5f * Mathf.Cos(angle), 3.5f * Mathf.Sin(angle), 0);
            Dot.transform.position = DotPosition + transform.position;
        }

        // 箭头方向调整
        Arrow.transform.localEulerAngles = new Vector3(0, 0, angle / Mathf.PI * 180 + 90);
        // 箭头长度调整
        // 如果圆点在圆的范围内
        if (vecMousePointToFish.magnitude <= 3.5f)
        {
            Arrow.transform.GetChild(0).transform.localPosition = new Vector3(0, vecMousePointToFish.magnitude - 3.5f, 0);
        }
        else
        {
            Arrow.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    private void addVelocityOnFish(Vector2 v)
    {
        v *= velocityMagnitude;
        rb.velocity = v;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionEable)
        {
            // 碰到窗户则过关
            if (collision.gameObject.name == "Window")
            {
                // 延迟0.2s后删除鱼，并切换到下一场景
                Invoke("NextLevel", 0.2f);
            }

            if (collision.collider.tag == "Ground")
            {
                onGround = true;
                sprender.sprite = jumpRenders[0];
            }
        }  
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CollisionEable)
        {
            if (collision.collider.tag == "Ground")
            {
                onGround = true;
            }
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CollisionEable)
        {
            if (collision.collider.tag == "Ground")
            {
                onGround = false;
            }
        }
    }

    void NextLevel()
    {
        Destroy(gameObject);
        //保存需要加载的目标场景  
        Globe.nextSceneName = "park";
        //异步加载场景
        SceneManager.LoadScene("LoadingScene");
    }
}
