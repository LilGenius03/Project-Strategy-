using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingDude : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public Transform target; //temp

    private List<GameObject> targets = new List<GameObject>();
    public Transform current_target;
    public Transform castle;

    public float damage;
    public float rate_of_fire;
    public float fire_delay;
    public float attack_range;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fire_delay = rate_of_fire;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_target != null)
        {
            if (fire_delay <= 0)
            {
                if (Vector3.Distance(transform.position, current_target.position) <= attack_range)
                {
                    current_target.GetComponentInParent<Health>().TakeDamage(damage);
                    Debug.Log("BANG!");
                }
                fire_delay = rate_of_fire;
            }
            else
                fire_delay -= Time.deltaTime;
        }
        else if (targets.Count > 0)
            GetNextTarget();

    }

    public virtual void GetNextTarget()
    {
        targets.RemoveAll(x => !x);
        if (targets.Count > 0)
            current_target = targets[0].transform;
        else
            current_target = castle;
        agent.SetDestination(current_target.position);
    }

    public void AddCastleAsTarget(Transform thecastle)
    {
        castle = thecastle;
        current_target = castle;
        agent.SetDestination(castle.position);
    }

    public virtual void OnNewTargetInArea(GameObject newTarget)
    {
        targets.Add(newTarget);
        if (current_target == null || current_target == castle)
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
                {
                    current_target = castle;
                    agent.SetDestination(castle.position);
                }
            }
                
                
        }
    }
}
