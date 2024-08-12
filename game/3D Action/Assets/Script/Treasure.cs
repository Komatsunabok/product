using UnityEngine;

// 宝物のスクリプト
public class Treasure : MonoBehaviour
{
    public GameObject effectPrefab;

    void Start()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;  // 宝物のColliderはトリガー
    }

    // 宝物が見つかった時（プレイキャラクターが宝物にぶつかったとき）
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Treasure found!");
            // 宝物を別の場所に移動
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            // 宝物を消す
            Destroy(gameObject);
        }
    }
}
