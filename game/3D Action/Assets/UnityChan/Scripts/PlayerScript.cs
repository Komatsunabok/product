using TMPro;
using UnityEngine;
//using UnityEngine.UI;


public class PlayerScripts : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    public TextMeshProUGUI scoreText;

    float normalSpeed = 3.0f;
    float sprintSpeed = 8.0f;
    float jump = 8.0f;
    float gravity = 10.0f;
    public int score = 0;

    Vector3 moveDirection = Vector3.zero;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        startPos = transform.position;

        //// Scoreタグが付いたゲームオブジェクトを探す
        //GameObject scoreObject = GameObject.FindWithTag("Score");
        //if (scoreObject != null)
        //{
        //    Debug.Log("Scoreタグのついたオブジェクトが見つかりました。");
        //    scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        //    if (scoreText != null)
        //    {
        //        Debug.Log("Textコンポーネントが見つかりました。");
        //        scoreText.text = "SCORE: 0"; // 初期スコアを設定
        //    }
        //    else
        //    {
        //        Debug.LogError("ScoreタグのオブジェクトにTextコンポーネントが見つかりませんでした。");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Scoreタグのついたオブジェクトが見つかりませんでした。");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed;

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
        if (other.gameObject.CompareTag("Treasure"))
        {
            score += 10;
            Debug.Log("Treasure found! score" + score);
            // 宝物を見つけたときの処理を追加
            
            

            //if (scoreText != null)
            //{
            //    scoreText.text = "SCORE:" + "" + score;
            //}
            //else
            //{
            //    Debug.LogError("scoreTextがnullです。");
            //}

            Destroy(other.gameObject);
        }
    }
}
