using TMPro;
using UnityEngine;

// �v���C�L�����N�^�[����̃X�N���v�g

public class PlayerScript : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    public TextMeshProUGUI scoreText;

    // �p�����[�^�ݒ�
    float normalSpeed = 3.0f; // �ʏ�ړ��X�s�[�h
    float sprintSpeed = 8.0f; // ���������̈ړ��X�s�[�h
    float speed;
    float jump = 8.0f; // �W�����v�̍���
    float gravity = 10.0f; // �d�͂̑傫��
    public int score = 0; // �l���X�R�A
    Vector3 moveDirection = Vector3.zero; // �ړ�����
    Vector3 startPos; // �����ʒu

    // �p�����[�^����̂��߂̕ϐ�
    public bool canRun = true; // true�̂Ƃ����邱�Ƃ��ł���
    //public float invincibleDuration = 3f; // �����r���i�G�ɂ������j
    //public float blinkInterval = 0.1f; // �����r���i�G�ɂ������j

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // �����ʒu�̐ݒ�
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        if (!canRun)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;
        }
        

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed;

        // �W�����v
        if (controller.isGrounded)
        {
            moveDirection = moveZ + moveX;
            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jump;
            }
        }
        else
        {
            moveDirection = moveZ + moveX + new Vector3(0, moveDirection.y, 0);
            moveDirection.y -= gravity * Time.deltaTime;
        }

        animator.SetFloat("MoveSpeed", (moveZ + moveX).magnitude);

        transform.LookAt(transform.position + moveZ + moveX);

        controller.Move(moveDirection * Time.deltaTime);
    }

    public void MoveStartPos()
    {
        controller.enabled = false;

        moveDirection = Vector3.zero;
        transform.position = startPos + Vector3.up * 10.0f;
        transform.rotation = Quaternion.identity;

        controller.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // �󕨂��������Ƃ�
        if (other.gameObject.CompareTag("Treasure"))
        {
            // �X�R�A���Z
            score += 10;
            Debug.Log("Treasure found! score" + score);
            
            // �󕨂�����
            Destroy(other.gameObject);
        }

    }

}
