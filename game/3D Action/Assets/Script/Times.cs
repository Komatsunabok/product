using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

// �������Ԃ��Ǘ����邽�߂̃X�N���v�g

public class Times : MonoBehaviour
{
    //�J�E���g�_�E��
    private const float timelimit = 60.0f; // ��������
    private float countdown; // �������ԁi�c�莞�ԁj

    int score; // �X�R�A

    public TextMeshProUGUI timeText; //���Ԃ�\������e�L�X�g

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
        //���Ԃ��J�E���g�_�E������
        countdown -= Time.deltaTime;

        //���Ԃ�\������
        UpdateCountdownText();

        //countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdown <= 0)
        {
            scoreObject.SetActive(false);
            timeObject.SetActive(false);

            // "Time UP!"�ƕ\��
            timeText.text = "Time UP!";

        }
    }

    //�@�������Ԃ��u00�i�b�j:00�v�̌`���ŕ\��
    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(countdown / 60);
        int seconds = Mathf.FloorToInt(countdown % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}