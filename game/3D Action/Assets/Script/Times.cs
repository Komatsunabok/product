using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

// 制限時間を管理するためのスクリプト

public class Times : MonoBehaviour
{
    //カウントダウン
    private const float timelimit = 60.0f; // 制限時間
    private float countdown; // 制限時間（残り時間）

    int score; // スコア

    public TextMeshProUGUI timeText; //時間を表示するテキスト

    GameObject gameplay = GameObject.FindWithTag("Gameplay");
    GameObject timeObject;
    GameObject scoreObject = GameObject.FindWithTag("Score");

    private void Start()
    {
        timeObject = gameObject;
        timeText = timeObject.GetComponent<TextMeshProUGUI>();

        countdown = timelimit;
        UpdateCountdownText();
        
        scoreObject.SetActive(true);
        timeObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;

        //時間を表示する
        UpdateCountdownText();

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
            scoreObject.SetActive(false);
            timeObject.SetActive(false);

            // "Time UP!"と表示
            timeText.text = "Time UP!";

        }
    }

    //　制限時間を「00（秒）:00」の形式で表示
    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(countdown / 60);
        int seconds = Mathf.FloorToInt(countdown % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}