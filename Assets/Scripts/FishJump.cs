using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJump : MonoBehaviour {

    // 最大出射速度对应的手指移动距离
    public float maxStretch;
    // 单位出射速度
    public float velocityMagnitude;

    // 相关物体
    public GameObject Fish;
    public GameObject Circle;
    public GameObject Dot;
    public GameObject Arrow;
    public GameObject FishSprite;
    // 播放音效物体
    //public GameObject SoundPlayer;

    public GameObject Tip1;
    public GameObject Tip2;

    // 鱼的Rigidbody组件
    private Rigidbody2D rb;
    // 鱼的SpriteRenderer组件
    private SpriteRenderer sprender;
    // 鱼的Fish组件
    private Fish FishScript;

    // 正在被点击
    public bool clicked = false;

    // 射线
    private Ray2D fishToLoosePointRay;
    // 出射距离
    Vector3 loosePoint;

    // 音效文件
    //public AudioClip Sound2;

    void Start() {

        rb = Fish.GetComponent<Rigidbody2D>();
        sprender = FishSprite.GetComponent<SpriteRenderer>();
        FishScript = Fish.GetComponent<Fish>();
        fishToLoosePointRay = new Ray2D(Fish.transform.position, Vector2.zero);

        //SoundPlayer = GameObject.Find("AudioController").transform.GetChild(2).gameObject;
    }
    
    void Update() {

        // 控制触摸的碰撞体跟鱼位置保持一致
        transform.position = Fish.transform.position + new Vector3(0, 0, -1);

        // 如果被点击，显示点击时的效果
        if (clicked)
        {
            updateFishRender();
            updateCircle();
        }

    }


    // 鼠标按下事件
    private void OnMouseDown()
    {
        // 鱼在地面上才可以点击
        if (!FishScript.onGround)
            return;
        clicked = true;

        // 显示点击效果
        Circle.SetActive(true);
        Dot.SetActive(true);
        Arrow.SetActive(true);

        // 关闭新手引导
        Tip1.SetActive(false);

        // 开始计时
        FishScript.sw.Start();
    }

    // 鼠标抬起事件
    private void OnMouseUp()
    {
        // 检测是否被点击
        if (!clicked)
            return;
        clicked = false;

        // 关闭点击效果
        Circle.SetActive(false);
        Dot.SetActive(false);
        Arrow.SetActive(false);

        //SoundPlayer.GetComponent<AudioSource>().PlayOneShot(Sound2);

        // 关闭新手引导
        Tip2.SetActive(false);

        // 鱼死后不可触发碰撞事件
        if (!FishScript.CollisionEable)
            return;

        // 鼠标位置
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        loosePoint = mouseWorldPoint;
        // 鼠标位置-鱼的位置
        Vector2 vecLoosePointToFish = loosePoint - Fish.transform.position;
        // 设置射线
        fishToLoosePointRay.origin = Fish.transform.position;
        fishToLoosePointRay.direction = vecLoosePointToFish;
        // 不能超过最大出射速度
        if (vecLoosePointToFish.magnitude > maxStretch)
            loosePoint = fishToLoosePointRay.GetPoint(maxStretch);
        // 给鱼添加出射速度
        Vector2 v = Fish.transform.position - loosePoint;
        addVelocityOnFish(v);

        // 发射时更新鱼的图片
        sprender.sprite = FishScript.jumpRenders[4];
        // 向右
        if (vecLoosePointToFish.x <= 0)
        {
            FishSprite.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        // 向左
        else
        {
            FishSprite.transform.localEulerAngles = new Vector3(0, 180, 0);
        }

    }


    // 鱼被点击时更新图片
    private void updateFishRender()
    {
        // 鼠标位置
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 鼠标位置-鱼的位置
        Vector2 vecMousePointToFish = mouseWorldPoint - Fish.transform.position;

        // 根据拉开距离的大小显示不同图片
        if (vecMousePointToFish.magnitude > 0 && vecMousePointToFish.magnitude < maxStretch / 3)
            sprender.sprite = FishScript.jumpRenders[1];
        else if (vecMousePointToFish.magnitude >= maxStretch / 3 && vecMousePointToFish.magnitude < 2 * maxStretch / 3)
            sprender.sprite = FishScript.jumpRenders[2];
        else
            sprender.sprite = FishScript.jumpRenders[3];
        // 向右
        if (vecMousePointToFish.x <= 0)
        {
            FishSprite.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        // 向左
        else
        {
            FishSprite.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        
    }

    // 鱼被点击时更新操作界面
    private void updateCircle()
    {
        // 鼠标位置
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 鼠标位置-鱼的位置
        Vector2 vecMousePointToFish = mouseWorldPoint - Fish.transform.position;
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
            Dot.transform.position = DotPosition + Fish.transform.position;
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

    // 给鱼施加初速度
    private void addVelocityOnFish(Vector2 v)
    {
        v *= velocityMagnitude;
        rb.velocity = v;
    }

    // 弹跳动画
    public void FishSpring()
    {
        StartCoroutine("Spring");
    }

    IEnumerator Spring()
    {
        Fish.GetComponent<Animator>().SetTrigger("jump");
        //Dont show player
        sprender.sprite = FishScript.jumpRenders[0];
        //Wait 0.2 second
        yield return new WaitForSeconds(0.07f);

        //Show player
        sprender.sprite = FishScript.jumpRenders[1];
        //Wait 0.2 second
        yield return new WaitForSeconds(0.07f);

        //Dont show player
        sprender.sprite = FishScript.jumpRenders[2];
        //Wait 0.2 second
        yield return new WaitForSeconds(0.07f);

        //Show player
        sprender.sprite = FishScript.jumpRenders[1];
        //Wait 0.2 second
        yield return new WaitForSeconds(0.07f);

        //Dont show player
        sprender.sprite = FishScript.jumpRenders[0];
        //Wait 0.2 second
        yield return new WaitForSeconds(0.07f);

        //Show player
        sprender.sprite = FishScript.jumpRenders[0];
    }
}
