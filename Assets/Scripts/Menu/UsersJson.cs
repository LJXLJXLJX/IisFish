using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

[System.Serializable]
public class User {

    public int UserID;
    public string UserName;
    public string PassWord;

    public User(int id, string userName, string passWord)
    {
        UserID = id;
        UserName = userName;
        PassWord = passWord;
    }

    public User()
    {
    }
}

[System.Serializable]
public class UsersList{
    
    public List<User> Users;

    public UsersList(List<User> users)
    {
        Users = users;
    }

    public UsersList()
    {
    }

}



public class UsersJson : MonoBehaviour{

    // 数据类
    UsersList UsersData;

    // json文件路径
    string appJsonPath;



    void Start() {

        //如果运行在编辑器中  
#if UNITY_EDITOR
        // json文件路径
        appJsonPath = Application.dataPath + "//Jsons/";

        //如果运行在Android设备中  
#elif UNITY_ANDROID
        // json文件路径
        appJsonPath = Application.persistentDataPath + "/";  

#endif

    }


    // 读取用户json文件
    public UsersList ConnectToJson()
    {
        // 如果已经有这个json文件,则读取
        if (File.Exists(appJsonPath + "UsersData.json"))
        {
            Debug.Log("读取用户json文件");
            string text = File.ReadAllText(appJsonPath + "UsersData.json");   //利用File类读取文件中的文本

            UsersData = JsonMapper.ToObject<UsersList>(text);    //利用JsonMapper将Json的文本转为我们要的UsersList类数据

            return UsersData;
        }

        // 如果没有，则创建json文件
        else
        {
            // 数据初始化
            User User0 = new User(0, "Admin", "123456");
            List<User> Userslist0 = new List<User>();
            Userslist0.Add(User0);
            UsersData = new UsersList(Userslist0);
            string json = JsonMapper.ToJson(UsersData);     //利用JsonMapper将用户信息转为Json格式的文本
            StreamWriter sw = new StreamWriter(appJsonPath + "UsersData.json"); //利用写入流创建文件
            sw.Write(json);     //写入数据
            sw.Close();     //关闭流
            sw.Dispose();	//销毁流

            return UsersData;
        }

    }

    // 将数据写入json文件
    public void WriteToJson(UsersList savedData)
    {
        string json = JsonMapper.ToJson(savedData);     //利用JsonMapper将用户信息转为Json格式的文本
        StreamWriter sw = new StreamWriter(appJsonPath + "UsersData.json"); //利用写入流创建文件
        sw.Write(json);     //写入数据
        sw.Close();     //关闭流
        sw.Dispose();	//销毁流
    }

    public string ReadUsernameByID(int id)
    {
        UsersData = ConnectToJson();
        string username = UsersData.Users[id].UserName;
        return username;
    }

    public string ReadPasswordByID(int id)
    {
        UsersData = ConnectToJson();
        string password = UsersData.Users[id].PassWord;
        return password;
    }

}
