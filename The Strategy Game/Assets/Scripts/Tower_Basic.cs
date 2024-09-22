using UnityEngine;

public class Tower_Basic : Tower
{
    RaycastHit hit;
    float fire_delay;

    private void Start()
    {
        fire_delay = rate_of_fire;
    }

    public override void Attack()
    {
        fire_pos.LookAt(current_target);

        if (fire_delay <= 0)
        {
            if(Physics.Raycast(fire_pos.position, fire_pos.forward, out hit, 10000f, ignore_layers))
            {
                Debug.Log("HIT: ");
                if (hit.transform.CompareTag("attack_unit"))
                {
                    Health targetHealth = hit.transform.GetComponent<Health>();
                    if (targetHealth)
                        targetHealth.TakeDamage(damage);

                }
            }
            Debug.DrawLine(fire_pos.position, hit.point, Color.red, 0.75f);
            fire_delay = rate_of_fire;
        }
        else
            fire_delay -= Time.deltaTime;
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
