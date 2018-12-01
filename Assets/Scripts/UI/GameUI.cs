using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Slider GameCanvas_ProgressBarSlider;

    public GameObject Fish;
    public float[] FishPositionXRange;
    
	void Start () {

        GameCanvas_ProgressBarSlider.value = 0;

    }
	
	void Update () {

            GameCanvas_ProgressBarSlider.value = (Fish.transform.position.x - FishPositionXRange[0]) / (FishPositionXRange[1] - FishPositionXRange[0]);

    }

    // ——————————UI事件——————————

    public void GameCanvas_BackButton_Onclick()
    {
        SceneManager.LoadScene("MainScene");
    }

}
