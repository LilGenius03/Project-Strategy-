using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AttackingDude : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public Transform target; //temp

    private List<GameObject> targets = new List<GameObject>();
    public Transform current_target;
    public Transform castle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCastleAsTarget(Transform thecastle)
    {
        castle = thecastle;
        agent.SetDestination(castle.position);
    }

    public virtual void OnNewTargetInArea(GameObject newTarget)
    {
        targets.Add(newTarget);
        if (current_target == null)
        {
            current_target = newTarget.transform;
            agent.SetDestination(current_target.position);
        }

    }

    public virtual void OnTargetExitArea(GameObject oldTarget)
    {
        targets.Remove(oldTarget);
        if (current_target == oldTarget)
        {
            if(targets.Count > 0)
            {
                current_target = targets[0].transform;
                agent.SetDestination(current_target.position);
            }
            else
            {
                current_target = null;
                if(castle != null)
                    agent.SetDestination(castle.position);
            }
                
                
        }
    }
}
