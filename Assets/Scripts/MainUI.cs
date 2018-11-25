using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour {

    UsersJson UsersJsonScript;

    // ——————————UI元素——————————

    public Canvas MenuCanvas;
    public Canvas LoginCanvas;
    public Canvas RegisterCanvas;
    public Canvas SettingsCanvas;
    public Canvas TipCanvas;
    public Canvas ChooseModeCanvas;


    public Text MenuCanvas_UserNameText;
    public InputField LoginCanvas_UserNameInputField;
    public InputField LoginCanvas_PasswordInputField;
    public InputField RegisterCanvas_UserNameInputField;
    public InputField RegisterCanvas_PasswordInputField1;
    public InputField RegisterCanvas_PasswordInputField2;
    public Slider SettingsCanvas_MusicSlider;
    public Slider SettingsCanvas_SoundSlider;

    public GameObject TipPrefab;



    void Start () {

        UsersJsonScript = GameObject.Find("GameController").GetComponent<UsersJson>();

        // 界面初始化
        MenuCanvas.enabled = true;
        LoginCanvas.enabled = false;
        RegisterCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        TipCanvas.enabled = true;
        ChooseModeCanvas.enabled = false;

        MenuCanvasDisplayUsername();

    }
	
	void Update () {
		
	}



    // SettingsCanvas显示设置的音量数据
    void SettingsCanvasDisplayMusicData()
    {
        // 判断 PlayerPrefs中是否存在这个key
        if (PlayerPrefs.HasKey("KEY_MusicSwitch"))
        {
            //存在就获取已有的值
            SettingsCanvas_MusicSlider.value = PlayerPrefs.GetInt("KEY_MusicSwitch", 0);
        }
        else
        {
            //不存在就设置为默认值
            PlayerPrefs.SetInt("KEY_SoundSwitch", 50);
            SettingsCanvas_MusicSlider.value = PlayerPrefs.GetInt("KEY_MusicSwitch", 0);
        }

        // 判断 PlayerPrefs中是否存在这个key
        if (PlayerPrefs.HasKey("KEY_SoundSwitch"))
        {
            //存在就获取已有的值
            SettingsCanvas_SoundSlider.value = PlayerPrefs.GetInt("KEY_SoundSwitch", 0);
        }
        else
        {
            //不存在就设置为默认值
            PlayerPrefs.SetInt("KEY_SoundSwitch", 50);
            SettingsCanvas_SoundSlider.value = PlayerPrefs.GetInt("KEY_SoundSwitch", 0);
        }
    }

    // MenuCanvas显示当前登录的用户名
    void MenuCanvasDisplayUsername()
    {
        // 读取当前登录的用户ID
        // 判断 PlayerPrefs中是否存在这个key
        if (PlayerPrefs.HasKey("KEY_UserID"))
        {
            //存在就获取已有的值
            MenuCanvas_UserNameText.text = UsersJsonScript.ReadUsernameByID(PlayerPrefs.GetInt("KEY_UserID", 0));
        }
        else
        {
            //不存在就设置为默认值
            PlayerPrefs.SetInt("KEY_UserID", 0);
            MenuCanvas_UserNameText.text = UsersJsonScript.ReadUsernameByID(PlayerPrefs.GetInt("KEY_UserID", 0));
        }
    }



    // ——————————UI事件——————————

    // ***********************
    // ——MenuCanvas
    // ***********************

    public void MenuCanvas_PlayButton_Onclick()
    {
        // 显示关卡选择界面
        MenuCanvas.enabled = false;
        ChooseModeCanvas.enabled = true;

        //// 跳转到Loading界面
        ////保存需要加载的目标场景  
        //Globe.nextSceneName = "GameScene";
        ////异步加载场景
        //SceneManager.LoadScene("LoadingScene");
    }

    public void MenuCanvas_UserButton_Onclick()
    {
        // 显示登录界面
        MenuCanvas.enabled = false;
        LoginCanvas.enabled = true;
        // 输入框中文字清空
        LoginCanvas_UserNameInputField.text = "";
        LoginCanvas_PasswordInputField.text = "";
    }

    public void MenuCanvas_SettingsButton_Onclick()
    {
        // 显示登录界面
        MenuCanvas.enabled = false;
        SettingsCanvas.enabled = true;
        // 滚动条显示信息
        SettingsCanvasDisplayMusicData();
    }

    // ***********************
    // ——LoginCanvas
    // ***********************

    public void LoginCanvas_CloseButton_Onclick()
    {
        // 关闭登录界面
        LoginCanvas.enabled = false;
        MenuCanvas.enabled = true;
    }

    public void LoginCanvas_RegisterButton_Onclick()
    {
        // 显示注册界面
        LoginCanvas.enabled = false;
        RegisterCanvas.enabled = true;
        // 输入框中文字清空
        RegisterCanvas_UserNameInputField.text = "";
        RegisterCanvas_PasswordInputField1.text = "";
        RegisterCanvas_PasswordInputField2.text = "";
    }

    public void LoginCanvas_LoginButton_Onclick()
    {
        // 读取用户输入的用户名和密码
        string username = LoginCanvas_UserNameInputField.text;
        string password = LoginCanvas_PasswordInputField.text;

        // 读取用户Json文件
        UsersList usersList = UsersJsonScript.ConnectToJson();
        // 遍历每一条用户信息
        for (int i = 0; i < usersList.Users.Count; i++)
        {
            // 如果用户名和密码都正确
            if (username == usersList.Users[i].UserName && password == usersList.Users[i].PassWord)
            {
                // 登录当前用户
                Debug.Log("登录成功，用户ID=" + usersList.Users[i].UserID);
                GameObject prefab = Instantiate(TipPrefab);
                prefab.transform.SetParent(TipCanvas.transform);
                prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
                prefab.transform.GetChild(1).GetComponent<Text>().text = "登录成功！";
                prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                PlayerPrefs.SetInt("KEY_UserID", usersList.Users[i].UserID);

                // 关闭登录界面
                LoginCanvas.enabled = false;
                MenuCanvas.enabled = true;
                // MenuCanvas显示当前登录的用户名
                MenuCanvasDisplayUsername();

                break;
            }
            // 没有匹配的用户名和密码数据
            if (i == usersList.Users.Count - 1)
            {
                Debug.Log("登录失败，用户名或密码错误");
                GameObject prefab = Instantiate(TipPrefab);
                prefab.transform.SetParent(TipCanvas.transform);
                prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
                prefab.transform.GetChild(1).GetComponent<Text>().text = "用户名或密码错误";
                prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    // ***********************
    // ——RegisterCanvas
    // ***********************

    public void RegisterCanvas_CloseButton_Onclick()
    {
        // 关闭注册界面
        RegisterCanvas.enabled = false;
        LoginCanvas.enabled = true;
    }

    public void RegisterCanvas_RegisterButton_Onclick()
    {
        // 读取用户输入的用户名和密码
        string username = RegisterCanvas_UserNameInputField.text;
        string password1 = RegisterCanvas_PasswordInputField1.text;
        string password2 = RegisterCanvas_PasswordInputField2.text;

        // 如果输入为空
        if (username == "" || password1 == "" || password2 == "")
        {
            GameObject prefab = Instantiate(TipPrefab);
            prefab.transform.SetParent(TipCanvas.transform);
            prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
            prefab.transform.GetChild(1).GetComponent<Text>().text = "输入不能为空！";
            prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Debug.Log("注册失败，输入不能为空");
        }
        else
        {
            // 读取用户Json文件
            UsersList usersList = UsersJsonScript.ConnectToJson();
            // 遍历每一条用户信息
            for (int i = 0; i < usersList.Users.Count; i++)
            {
                // 如果输入的用户名已经注册
                if (username == usersList.Users[i].UserName)
                {
                    Debug.Log("注册失败，用户名已经注册" + i + usersList.Users.Count);
                    GameObject prefab = Instantiate(TipPrefab);
                    prefab.transform.SetParent(TipCanvas.transform);
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
                    prefab.transform.GetChild(1).GetComponent<Text>().text = "该用户名已被注册！";
                    prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    break;
                }

                // 没有匹配的用户名
                if (i == usersList.Users.Count - 1)
                {
                    // 如果两次密码一致
                    if (password1 == password2)
                    {
                        User newUser = new User(i + 1, username, password1);
                        usersList.Users.Add(newUser);
                        UsersJsonScript.WriteToJson(usersList);
                        Debug.Log("注册成功，用户ID=" + (i + 1));
                        GameObject prefab = Instantiate(TipPrefab);
                        prefab.transform.SetParent(TipCanvas.transform);
                        prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
                        prefab.transform.GetChild(1).GetComponent<Text>().text = "注册成功！";
                        prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                        // 关闭注册界面
                        RegisterCanvas.enabled = false;
                        LoginCanvas.enabled = true;
                    }
                    else
                    {
                        Debug.Log("注册失败，两次密码不一致");
                        GameObject prefab = Instantiate(TipPrefab);
                        prefab.transform.SetParent(TipCanvas.transform);
                        prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
                        prefab.transform.GetChild(1).GetComponent<Text>().text = "注册失败，两次密码不一致";
                        prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    }

                    break;
                }
            }
        }

    }

    // ***********************
    // ——SettingsCanvas
    // ***********************

    public void SettingsCanvas_BackButton_Onclick()
    {
        // 关闭设置界面
        SettingsCanvas.enabled = false;
        MenuCanvas.enabled = true;
    }

    public void SettingsCanvas_YesButton_Onclick()
    {
        // 关闭设置界面
        SettingsCanvas.enabled = false;
        MenuCanvas.enabled = true;

        // 将设置值写入PlayerPrefs
        PlayerPrefs.SetInt("KEY_MusicSwitch", (int)(SettingsCanvas_MusicSlider.value));
        PlayerPrefs.SetInt("KEY_SoundSwitch", (int)(SettingsCanvas_SoundSlider.value));
        GameObject prefab = Instantiate(TipPrefab);
        prefab.transform.SetParent(TipCanvas.transform);
        prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
        prefab.transform.GetChild(1).GetComponent<Text>().text = "设置已保存";
        prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }

    // ***********************
    // ——ChooseModeCanvas
    // ***********************

    public void ChooseModeCanvas_BackButton_Onclick()
    {
        // 关闭设置界面
        ChooseModeCanvas.enabled = false;
        MenuCanvas.enabled = true;
    }

    public void ChooseModeCanvas_Mode1Button_Onclick()
    {

        // 跳转到Loading界面
        //保存需要加载的目标场景  
        Globe.nextSceneName = "kitchen";
        //异步加载场景
        SceneManager.LoadScene("LoadingScene");
    }

    public void ChooseModeCanvas_Mode2Button_Onclick()
    {
        GameObject prefab = Instantiate(TipPrefab);
        prefab.transform.SetParent(TipCanvas.transform);
        prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
        prefab.transform.GetChild(1).GetComponent<Text>().text = "敬请期待！";
        prefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}
