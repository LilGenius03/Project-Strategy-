using UnityEngine;

public class Tower_Bomber : Tower
{

    [SerializeField] GameObject bomb;
    float fire_delay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fire_delay = rate_of_fire / 2;
    }

    public override void Attack()
    {
        if (fire_delay <= 0)
        {
            Debug.Log("I SHOULD BE SHOOTING!!!>>!>?W<!L:");
            Instantiate(bomb, current_target.position, Quaternion.identity);
            StartTracerCurve(current_target.position);
            fire_delay = rate_of_fire;
        }
        else
            fire_delay -= Time.deltaTime;
    }

    public override void GetNextTarget()
    {
        base.GetNextTarget();
    }

    public override void OnNewTargetInArea(GameObject newTarget)
    {
        base.OnNewTargetInArea(newTarget);
    }

    public override void OnTargetExitArea(GameObject oldTarget)
    {
        base.OnTargetExitArea(oldTarget);
    }
}
