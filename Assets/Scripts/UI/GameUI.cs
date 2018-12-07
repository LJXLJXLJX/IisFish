using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    // UI元素
    public Canvas GameCanvas;
    public Canvas FinishCanvas;
    public Slider GameCanvas_ProgressBarSlider;
    public Text GameCanvas_TimerText;
    public Text GameCanvas_DeathText;
    public Text FinishCanvas_TimerText;
    public Text FinishCanvas_DeathText;

    // 时间参数
    int timeMinutes;
    int timeSeconds;
    string textMinutes;
    string textSeconds;
    // 死亡次数
    int death;

    // Fish物体
    public GameObject Fish;
    // Fish物体x坐标范围
    public float[] FishPositionXRange;
    // Fish脚本
    Fish FishScript;



    void Start () {

        // 链接脚本
        FishScript = Fish.GetComponent<Fish>();

        // 界面初始化
        GameCanvas.enabled = true;
        FinishCanvas.enabled = false;
        GameCanvas_ProgressBarSlider.value = 0;

        // 时间初始化
        timeMinutes = 0;
        timeSeconds = 0;

    }
	
	void Update () {

        // 显示关卡进度
        GameCanvas_ProgressBarSlider.value = (Fish.transform.position.x - FishPositionXRange[0]) / (FishPositionXRange[1] - FishPositionXRange[0]);

        //更改时间显示
        timeMinutes = FishScript.timeMinutes;
        timeSeconds = FishScript.timeSeconds;

        if (timeMinutes >= 10)
        {
            textMinutes = timeMinutes.ToString();
        }
        else
        {
            textMinutes = "0" + timeMinutes.ToString();
        }
        if (timeSeconds >= 10)
        {
            textSeconds = timeSeconds.ToString();
        }
        else
        {
            textSeconds = "0" + timeSeconds.ToString();
        }

        GameCanvas_TimerText.text = textMinutes + ":" + textSeconds;

        // 更改死亡次数显示
        death = FishScript.Death;
        GameCanvas_DeathText.text = "" + death;
    }

    public void ShowFinishCanvas()
    {
        FinishCanvas.enabled = true;
        GameCanvas.enabled = false;

        FinishCanvas_TimerText.text = "用时：" + textMinutes + ":" + textSeconds;
        FinishCanvas_DeathText.text = "死亡 " + death + " 次";
    }



    // ——————————UI事件——————————

    // ************
    // GameCanvas
    // ************
    public void GameCanvas_BackButton_Onclick()
    {
        SceneManager.LoadScene("MainScene");
    }

    // ************
    // FinishCanvas
    // ************
    public void FinishCanvas_ContinueButton_Onclick()
    {
        //保存需要加载的目标场景  
        Globe.nextSceneName = "park";
        //异步加载场景
        SceneManager.LoadScene("LoadingScene");
    }


}
