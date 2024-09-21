using UnityEngine;
using UnityEngine.AI;

public class AttackingDude : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(PlayerManager.instance.castle.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
