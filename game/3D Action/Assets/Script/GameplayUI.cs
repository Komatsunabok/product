using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

// ゲームプレイ時のUI

public class GamePlayUI : MonoBehaviour
{
    //カウントダウン
    private const float timelimit = 60.0f;
    private float countdown;

    // スコア・制限時間
    public TextMeshProUGUI scoreText; // スコアテキスト
    public TextMeshProUGUI timeText; // 制限時間テキスト

    GameObject gameplay;
    GameObject scoreObject;
    GameObject timeObject;
    GameObject playerObject;
    PlayerScript playerScript;
    GameObject result;
    GameObject treasures;

    private void Start()
    {
        gameplay = gameObject;
        scoreObject = GameObject.FindWithTag("Score");
        timeObject = GameObject.FindWithTag("Time");
        playerObject = GameObject.FindWithTag("Player");
        treasures = GameObject.FindWithTag("Treasures");

        gameObject.SetActive(true);

        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        timeText = timeObject.GetComponent<TextMeshProUGUI>();
        playerScript = playerObject.GetComponent<PlayerScript>();


        // スコアテキスト初期化
        scoreText.text = "SCORE: " + playerScript.score;

        // 制限時間初期化
        countdown = timelimit;
        UpdateCountdownText();
    }

    // Update is called once per frame
    void Update()
    {
        // スコアテキストの更新
        if (playerScript != null)
        {
            scoreText.text = "SCORE: " + playerScript.score;
        }

        // カウントダウン
        countdown -= Time.deltaTime;
        UpdateCountdownText();

        // countdownが0以下になったとき
        if (countdown <= 0)
        {
            SaveScore();
            SceneManager.LoadScene("ResultScene");

        }
    }

    // カウントダウンのための関数
    void UpdateCountdownText()
    {
        float seconds = countdown % 60; // 秒数をfloat型で取得する

        if (timeText != null)
        {
            timeText.text = string.Format("{0:00.00}", seconds);
        }
    }

    // スコアをほぞんする関数
    public void SaveScore()
    {
        PlayerPrefs.SetInt("score", playerScript.score);
        PlayerPrefs.Save();
    }
}
