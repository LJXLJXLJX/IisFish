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
    public Text FinishCanvas_BestText;

    // 时间参数
    int timeMinutes;
    int timeSeconds;
    string textMinutes;
    string textSeconds;
    // 死亡次数
    int death;
    // 最高记录
    int best;

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

        //初始化
        // 判断 PlayerPrefs中是否存在这个key
        if (PlayerPrefs.HasKey("KEY_Best"))
        {
            //存在就获取已有的值
            best = PlayerPrefs.GetInt("KEY_Best", 0);
        }
        else
        {
            //不存在就设置为默认值
            best = 999;
            PlayerPrefs.SetInt("KEY_Best", best);
        }

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

        //更新最高记录
        if ((timeMinutes * 60 + timeSeconds) < best)
        {
            best = timeMinutes * 60 + timeSeconds;
            PlayerPrefs.SetInt("KEY_Best", best);
        }

        int bestMin = best / 60;
        int bestSec = best - best / 60 * 60;

        string bestMinStr;
        string bestSecStr;

        if (bestMin >= 10)
        {
            bestMinStr = bestMin.ToString();
        }
        else
        {
            bestMinStr = "0" + bestMin.ToString();
        }
        if (bestSec >= 10)
        {
            bestSecStr = bestSec.ToString();
        }
        else
        {
            bestSecStr = "0" + bestSec.ToString();
        }

        FinishCanvas_BestText.text = "最高记录：" + bestMinStr + ":" + bestSecStr;
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
