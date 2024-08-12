using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

// �Q�[���v���C����UI

public class GamePlayUI : MonoBehaviour
{
    //�J�E���g�_�E��
    private const float timelimit = 60.0f;
    private float countdown;

    // �X�R�A�E��������
    public TextMeshProUGUI scoreText; // �X�R�A�e�L�X�g
    public TextMeshProUGUI timeText; // �������ԃe�L�X�g

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


        // �X�R�A�e�L�X�g������
        scoreText.text = "SCORE: " + playerScript.score;

        // �������ԏ�����
        countdown = timelimit;
        UpdateCountdownText();
    }

    // Update is called once per frame
    void Update()
    {
        // �X�R�A�e�L�X�g�̍X�V
        if (playerScript != null)
        {
            scoreText.text = "SCORE: " + playerScript.score;
        }

        // �J�E���g�_�E��
        countdown -= Time.deltaTime;
        UpdateCountdownText();

        // countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdown <= 0)
        {
            SaveScore();
            SceneManager.LoadScene("ResultScene");

        }
    }

    // �J�E���g�_�E���̂��߂̊֐�
    void UpdateCountdownText()
    {
        float seconds = countdown % 60; // �b����float�^�Ŏ擾����

        if (timeText != null)
        {
            timeText.text = string.Format("{0:00.00}", seconds);
        }
    }

    // �X�R�A���ق��񂷂�֐�
    public void SaveScore()
    {
        PlayerPrefs.SetInt("score", playerScript.score);
        PlayerPrefs.Save();
    }
}
