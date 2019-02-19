using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Fish : MonoBehaviour {

    // 鱼死后不可触发碰撞
    [SerializeField]
    public bool CollisionEable;
    // 鱼正在地面上
    [SerializeField]
    public bool onGround = false;

    // 鱼的Rigidbody组件
    private Rigidbody2D rb;
    // 鱼的SpriteRenderer组件
    private SpriteRenderer sprender;

    // 鱼的对话气泡
    public GameObject Speak;
    // 鱼显示图片的物体
    public GameObject FishSprite;
    // FishJump物体
    public GameObject FishJump;
    // 播放音效物体
    public GameObject SoundPlayer;

    //// 鱼的初始位置
    //private Vector2 originalPos;
    // 复活点坐标
    public Vector2[] RelivePoints;
    // 当前应该复活的点坐标
    Vector2 RelivePos;

    // 鱼跳跃动作的分解图片
    public Sprite[] jumpRenders;
    // 表情气泡图片
    public Sprite[] SpeakSprites;

    // 无操作计时器
    float Timer1;
    // 关卡总计时器
    public Stopwatch sw = new Stopwatch();
    public int timeMinutes;
    public int timeSeconds;

    // 死亡次数
    public int Death;

    // 音效文件
    public AudioClip Sound1;



    void Start () {

        //originalPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprender = FishSprite.GetComponent<SpriteRenderer>();

        // 初始化
        CollisionEable = false;
        ResetPosition();

        Timer1 = 0;
        sw.Stop();
        sw.Reset();
        Death = 0;

        SoundPlayer = GameObject.Find("AudioController").transform.GetChild(1).gameObject;
    }
	
	void Update () {

        // 获取计时器的分和秒值
        timeMinutes = sw.Elapsed.Minutes;
        timeSeconds = sw.Elapsed.Seconds;

        // 无操作计时
        if (FishJump.GetComponent<FishJump>().clicked == false)
        {
            Timer1 += Time.deltaTime;

            // 5s未操作播放动画
            if (Timer1 >= 5)
            {
                Timer1 = 0;
                Speak.SetActive(false);
                Speak.GetComponent<SpriteRenderer>().sprite = SpeakSprites[Random.Range(1, 4)];
                Speak.SetActive(true);
                Invoke("SpeakDisappear", 1.5f);
            }
        }
        else
        {
            Timer1 = 0;
        }
    }



    // 碰撞事件
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionEable)
        {
            Timer1 = 0;

            // 死亡线
            if (collision.collider.name == "deathLine")
            {
                // 死亡事件
                fishDie();
            }

            // 窗户
            if (collision.gameObject.name == "Window")
            {
                // 延迟0.2s后删除鱼，并切换到下一场景
                Invoke("NextLevel", 0.2f);
            }

            // 地面
            if (collision.collider.tag == "Ground")
            {
                SoundPlayer.GetComponent<AudioSource>().PlayOneShot(Sound1);

                onGround = true;
                gameObject.transform.GetChild(0).GetComponent<FishJump>().FishSpring();

                if (collision.gameObject.name == "water")
                {
                    Speak.SetActive(false);
                    Speak.GetComponent<SpriteRenderer>().sprite = SpeakSprites[5];
                    Speak.SetActive(true);
                    Invoke("SpeakDisappear", 1.5f);
                }
            }

            // 向左反弹
            if (collision.collider.tag == "ReboundLeft")
            {
                SoundPlayer.GetComponent<AudioSource>().PlayOneShot(Sound1);

                rb.AddForce(new Vector2 (-400, 0));
                Speak.SetActive(false);
                Speak.GetComponent<SpriteRenderer>().sprite = SpeakSprites[4];
                Speak.SetActive(true);
                Invoke("SpeakDisappear", 1.5f);
            }

            // 向右反弹
            if (collision.collider.tag == "ReboundRight")
            {
                SoundPlayer.GetComponent<AudioSource>().PlayOneShot(Sound1);

                rb.AddForce(new Vector2(400, 0));
                Speak.SetActive(false);
                Speak.GetComponent<SpriteRenderer>().sprite = SpeakSprites[4];
                Speak.SetActive(true);
                Invoke("SpeakDisappear", 1.5f);
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

    // 死亡事件
    public void fishDie()
    {
        Death++;

        // 播放死亡动画
        StartCoroutine("Hit");

        // 死亡对话气泡
        Speak.SetActive(false);
        Speak.GetComponent<SpriteRenderer>().sprite = SpeakSprites[0];
        Speak.SetActive(true);

        // 回到初始位置
        Invoke("ResetPosition", 1.5f);

        // 禁止碰撞事件
        CollisionEable = false;

        // 设置角色图片
        sprender.sprite = jumpRenders[0];

    }

    // 鱼的初始化
    void ResetPosition()
    {
        // 速度归零
        rb.velocity = Vector3.zero;
        //// 位置初始化
        //transform.position = originalPos;
        // 回到复活点
        if (transform.position.x <= RelivePoints[1].x)
        {
            RelivePos = RelivePoints[0];
        }
        else if (transform.position.x <= RelivePoints[2].x)
        {
            RelivePos = RelivePoints[1];
        }
        else
        {
            RelivePos = RelivePoints[2];
        }
        transform.position = RelivePos;
        // 对话气泡消失
        Speak.SetActive(false);
        // 开始碰撞使能
        Invoke("OpenCollisionEable", 0.3f);
        // 图片方向初始化
        FishSprite.transform.localEulerAngles = new Vector3(0, 0, 0);
        // 图片初始化
        sprender.sprite = jumpRenders[0];
    }

    void SpeakDisappear()
    {
        Speak.SetActive(false);
    }

    void OpenCollisionEable()
    {
        CollisionEable = true;
    }

    void NextLevel()
    {
        GameObject.Find("Main Camera").GetComponent<CameraControl>().CameraFollow = false;
        gameObject.transform.position = RelivePoints[0];
        //Destroy(gameObject);

        // 显示结算界面
        GameObject.Find("UIs").GetComponent<GameUI>().ShowFinishCanvas();
    }

    // 死亡动画
    IEnumerator Hit()
    {
        //Dont show player
        sprender.color = Color.red;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Show player
        sprender.color = Color.white;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Dont show player
        sprender.color = Color.red;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Show player
        sprender.color = Color.white;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Dont show player
        sprender.color = Color.red;
        //Wait 0.2 second
        yield return new WaitForSeconds(0.2f);

        //Show player
        sprender.color = Color.white;
    }

}
