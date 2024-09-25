using NUnit.Framework;
using System.Collections;
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
    public LineRenderer lr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (current_target != null)
        {
            Attack();
        }
        else if (targets.Count > 0)
            GetNextTarget();
    }

    public virtual void Attack()
    {

    }

    public virtual void GetNextTarget()
    {
        targets.RemoveAll(x => !x);
        if (targets.Count > 0)
            current_target = targets[0].transform;
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
        {
            if (targets.Count > 0)
                current_target = targets[0].transform;
            else
                current_target = null;
        }

    }

    public void StartTracer(Vector3 pos)
    {
        StartCoroutine(SpawnTracer(pos));
    }

    public IEnumerator SpawnTracer(Vector3 pos)
    {
        lr.SetPosition(0, fire_pos.position);
        lr.SetPosition(1, pos);
        yield return new WaitForSecondsRealtime(0.5f);
        lr.SetPosition(1, fire_pos.position);
    }

    public void StartTracerCurve(Vector3 pos)
    {
        StartCoroutine(SpawnTracerCurve(pos));
    }

    public IEnumerator SpawnTracerCurve(Vector3 pos)
    {
        lr.SetPosition(0, fire_pos.position);
        Vector3 mid_pos = Vector3.Lerp(fire_pos.position, pos, 0.5f);
        mid_pos.y += mid_pos.y;
        Debug.Log(Vector3.Lerp(fire_pos.position, pos, 0.5f));
        lr.SetPosition(1, mid_pos);
        lr.SetPosition(2, pos);
        yield return new WaitForSecondsRealtime(0.5f);
        lr.SetPosition(1, fire_pos.position);
        lr.SetPosition(2, fire_pos.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(fire_pos.position, 0.1f);
    }
}
