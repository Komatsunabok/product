using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ŽÀ‘•“r’†
// “G‚ÌƒXƒNƒŠƒvƒg

public class EnemyScript : MonoBehaviour
{
    float ypos = 1.2f;
    public GameObject target;
    private NavMeshAgent agent;
    public GameObject floor;
    Vector3 floorSize;
    public float distance = 100;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = randomMove();

        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");

        floor = GameObject.FindWithTag("Floor");
        Renderer floorRenderer = floor.GetComponent<Renderer>();
        floorSize = floorRenderer.bounds.size;

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);    
        if (distance < 10)
        {
            agent.destination = target.transform.position;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = randomMove();
            while(Physics.OverlapSphere(pos, 0).Length != 0)
            {
                pos = randomMove();
            }
            transform.position = pos;
        }
    }

    //

    Vector3 randomMove()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-floorSize.x / 2, floorSize.x / 2),
            ypos,
            Random.Range(-floorSize.z / 2, floorSize.z / 2)
        );

        return randomPosition;
    }
}
