using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// リザルト画面のUI

public class ResultUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // スコアテキスト

    GameObject scoreObject;

    // ボタン
    public Button escapeButton; // エスケープボタン（ゲームを終了）
    public Button retryButton; // リトライボタン

    private Button selectedButton; // どのボタンが選択されているか管理する変数

    void Start()
    {
        // スコア表示
        scoreObject = GameObject.FindWithTag("FinalScore");
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        int score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "SCORE: " + score.ToString();

        // ボタンの設定
        escapeButton = GameObject.FindWithTag("EscapeButton")?.GetComponent<Button>();
        retryButton = GameObject.FindWithTag("RetryButton")?.GetComponent<Button>();
        escapeButton.onClick.AddListener(OnEscapeButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);

        // 初期選択ボタンの設定
        selectedButton = retryButton;
        HighlightButton(selectedButton);
    }

    // Update is called once per frame
    void Update()
    {
        // ボタン選択
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 選択ボタンの切り替え
            selectedButton = (selectedButton == escapeButton) ? retryButton : escapeButton;
            HighlightButton(selectedButton);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // スペースキーで選択中のボタンをクリック
            selectedButton.onClick.Invoke();
        }
    }

    // 選択中のボタンをハイライト
    private void HighlightButton(Button button)
    {
        // 全てのボタンの選択状態を解除
        EventSystem.current.SetSelectedGameObject(null);

        // 選択中のボタンを強調表示
        button.GetComponent<Image>().color = Color.white; // 選択されたボタンの色を黄色に変更
        EventSystem.current.SetSelectedGameObject(button.gameObject);

        // 非選択のボタンの色を元に戻す
        if (button == escapeButton)
        {
            retryButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            escapeButton.GetComponent<Image>().color = Color.gray;
        }
    }

    // エスケープボタンを押したときの処理
    private void OnEscapeButtonClicked()
    {
        // ゲーム終了処理
        Application.Quit();
    }

    // リトライボタンを押したときの処理
    private void OnRetryButtonClicked()
    {
        // スタート画面に戻る
        SceneManager.LoadScene("StartScene");
    }
}
