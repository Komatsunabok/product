using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// �X�^�[�g��ʂ�UI
public class StartScene : MonoBehaviour
{
    // �X�^�[�g�{�^��
    public Button startButton;

    void Start()
    {
        startButton = GameObject.Find("StartButton")?.GetComponent<Button>();
        startButton.onClick.AddListener(change_button);
    }
    void Update()
    {
        // �X�y�[�X�L�[�Ń{�^��������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (startButton != null)
            {
                startButton.onClick.Invoke();
            }
        }
    }

    // �{�^�����������Ƃ��̏���
    public void change_button()
    {
        // �Q�[���v���C�̃V�[���ɑJ��
        SceneManager.LoadScene("SampleScene");
    }

}
