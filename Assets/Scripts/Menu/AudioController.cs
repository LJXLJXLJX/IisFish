using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    // 此脚本永不消毁，并且每次进入初始场景时进行判断，若存在重复的则销毁  
    static AudioController BgmController_instance = null;
    void Awake()
    {

        if (BgmController_instance == null)
        {
            BgmController_instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != BgmController_instance)
        {
            Destroy(gameObject);
        }
    }

    // 音乐音量
    public int MusicVolume;
    // 音效音量
    public int SoundVolume;

    // 播放音乐的物体
    public GameObject BgmPlayer;
    public GameObject SoundPlayer1;
    public GameObject SoundPlayer2;


    void Start () {

        //初始化
        // 判断 PlayerPrefs中是否存在这个key
        if (PlayerPrefs.HasKey("KEY_MusicSwitch"))
        {
            //存在就获取已有的值
            MusicVolume = PlayerPrefs.GetInt("KEY_MusicSwitch", 0);
        }
        else
        {
            //不存在就设置为默认值
            MusicVolume = 50;
            PlayerPrefs.SetInt("KEY_MusicSwitch", MusicVolume);
        }

        // 判断 PlayerPrefs中是否存在这个key
        if (PlayerPrefs.HasKey("KEY_SoundSwitch"))
        {
            //存在就获取已有的值
            SoundVolume = PlayerPrefs.GetInt("KEY_SoundSwitch", 0);
        }
        else
        {
            //不存在就设置为默认值
            SoundVolume = 50;
            PlayerPrefs.SetInt("KEY_SoundSwitch", SoundVolume);
        }
    }
	
	void Update () {

        // 设置bgm音量
        MusicVolume = PlayerPrefs.GetInt("KEY_MusicSwitch", 0);
        BgmPlayer.GetComponent<AudioSource>().volume = MusicVolume / 100.0f;

        // 设置音效音量
        SoundVolume = PlayerPrefs.GetInt("KEY_SoundSwitch", 0);
        SoundPlayer1.GetComponent<AudioSource>().volume = SoundVolume / 100.0f;
        SoundPlayer2.GetComponent<AudioSource>().volume = SoundVolume / 100.0f;

    }

}
