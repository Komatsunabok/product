using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// スタート画面のUI
public class StartScene : MonoBehaviour
{
    // スタートボタン
    public Button startButton;

    void Start()
    {
        startButton = GameObject.Find("StartButton")?.GetComponent<Button>();
        startButton.onClick.AddListener(change_button);
    }
    void Update()
    {
        // スペースキーでボタンを押す
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (startButton != null)
            {
                startButton.onClick.Invoke();
            }
        }
    }

    // ボタンを押したときの処理
    public void change_button()
    {
        // ゲームプレイのシーンに遷移
        SceneManager.LoadScene("SampleScene");
    }

}
