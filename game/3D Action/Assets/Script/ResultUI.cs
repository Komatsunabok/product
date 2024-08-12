using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// ���U���g��ʂ�UI

public class ResultUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // �X�R�A�e�L�X�g

    GameObject scoreObject;

    // �{�^��
    public Button escapeButton; // �G�X�P�[�v�{�^���i�Q�[�����I���j
    public Button retryButton; // ���g���C�{�^��

    private Button selectedButton; // �ǂ̃{�^�����I������Ă��邩�Ǘ�����ϐ�

    void Start()
    {
        // �X�R�A�\��
        scoreObject = GameObject.FindWithTag("FinalScore");
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        int score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "SCORE: " + score.ToString();

        // �{�^���̐ݒ�
        escapeButton = GameObject.FindWithTag("EscapeButton")?.GetComponent<Button>();
        retryButton = GameObject.FindWithTag("RetryButton")?.GetComponent<Button>();
        escapeButton.onClick.AddListener(OnEscapeButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);

        // �����I���{�^���̐ݒ�
        selectedButton = retryButton;
        HighlightButton(selectedButton);
    }

    // Update is called once per frame
    void Update()
    {
        // �{�^���I��
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // �I���{�^���̐؂�ւ�
            selectedButton = (selectedButton == escapeButton) ? retryButton : escapeButton;
            HighlightButton(selectedButton);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �X�y�[�X�L�[�őI�𒆂̃{�^�����N���b�N
            selectedButton.onClick.Invoke();
        }
    }

    // �I�𒆂̃{�^�����n�C���C�g
    private void HighlightButton(Button button)
    {
        // �S�Ẵ{�^���̑I����Ԃ�����
        EventSystem.current.SetSelectedGameObject(null);

        // �I�𒆂̃{�^���������\��
        button.GetComponent<Image>().color = Color.white; // �I�����ꂽ�{�^���̐F�����F�ɕύX
        EventSystem.current.SetSelectedGameObject(button.gameObject);

        // ��I���̃{�^���̐F�����ɖ߂�
        if (button == escapeButton)
        {
            retryButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            escapeButton.GetComponent<Image>().color = Color.gray;
        }
    }

    // �G�X�P�[�v�{�^�����������Ƃ��̏���
    private void OnEscapeButtonClicked()
    {
        // �Q�[���I������
        Application.Quit();
    }

    // ���g���C�{�^�����������Ƃ��̏���
    private void OnRetryButtonClicked()
    {
        // �X�^�[�g��ʂɖ߂�
        SceneManager.LoadScene("StartScene");
    }
}
