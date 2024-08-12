using TMPro;
using UnityEngine;

// プレイキャラクター制御のスクリプト

public class PlayerScript : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    public TextMeshProUGUI scoreText;

    // パラメータ設定
    float normalSpeed = 3.0f; // 通常移動スピード
    float sprintSpeed = 8.0f; // 走った時の移動スピード
    float speed;
    float jump = 8.0f; // ジャンプの高さ
    float gravity = 10.0f; // 重力の大きさ
    public int score = 0; // 獲得スコア
    Vector3 moveDirection = Vector3.zero; // 移動方向
    Vector3 startPos; // 初期位置

    // パラメータ制御のための変数
    public bool canRun = true; // trueのとき走ることができる
    //public float invincibleDuration = 3f; // 実装途中（敵にかかわる）
    //public float blinkInterval = 0.1f; // 実装途中（敵にかかわる）

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 初期位置の設定
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // 移動
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

        // ジャンプ
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
        // 宝物を見つけたとき
        if (other.gameObject.CompareTag("Treasure"))
        {
            // スコア加算
            score += 10;
            Debug.Log("Treasure found! score" + score);
            
            // 宝物を消す
            Destroy(other.gameObject);
        }

    }

}
