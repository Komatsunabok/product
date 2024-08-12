using UnityEngine;

// �󕨂̃X�N���v�g
public class Treasure : MonoBehaviour
{
    public GameObject effectPrefab;

    void Start()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;  // �󕨂�Collider�̓g���K�[
    }

    // �󕨂������������i�v���C�L�����N�^�[���󕨂ɂԂ������Ƃ��j
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Treasure found!");
            // �󕨂�ʂ̏ꏊ�Ɉړ�
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            // �󕨂�����
            Destroy(gameObject);
        }
    }
}
