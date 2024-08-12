using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            player.MoveStartPos();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
