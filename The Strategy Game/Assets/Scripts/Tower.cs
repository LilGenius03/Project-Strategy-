using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Health health;

    private List<GameObject> targets = new List<GameObject>();
    public Transform current_target;

    public LayerMask ignore_layers;
    public Transform fire_pos;
    public float rate_of_fire;
    public float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if(current_target != null)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {

    }

    public virtual void OnNewTargetInArea(GameObject newTarget)
    {
        targets.Add(newTarget);
        if (current_target == null)
            current_target = newTarget.transform;
    }

    public virtual void OnTargetExitArea(GameObject oldTarget)
    {
        targets.Remove(oldTarget);
        if (current_target == oldTarget)
            current_target = targets[0].transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(fire_pos.position, 0.1f);
    }
}
