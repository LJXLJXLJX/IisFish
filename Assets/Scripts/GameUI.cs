using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
		
	}

    // ——————————UI事件——————————

    public void GameCanvas_BackButton_Onclick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
