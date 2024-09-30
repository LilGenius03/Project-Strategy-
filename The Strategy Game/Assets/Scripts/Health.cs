using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnHeal, OnTakeDamage, OnDie;

    PlayerController otherPlayer;
    public int pID;

    [SerializeField] int dude_index;

    [SerializeField] float max_health = 10;
    private float current_health;

    [SerializeField] int dmg_gold, death_gold;

    [SerializeField] bool isCastle;

    [SerializeField] MeshRenderer[] healthMeshes; //temp
    [SerializeField] Gradient healthColorGradient;

    bool isdead;

    private void Start()
    {
        current_health = max_health;
        if(!isCastle)
            otherPlayer = PlayerManager.instance.players[pID].GetComponent<PlayerController>();
    }

    public void Heal(float amount)
    {
        current_health += amount;
        current_health = Mathf.Ceil(current_health);
        if (current_health > max_health)
            current_health = max_health;
    }

    public void TakeDamage(float amount)
    {
        current_health -= amount;
        current_health = Mathf.Floor(current_health);
        SetHealthColor();
        OnTakeDamage.Invoke();
        if (otherPlayer != null)
        {
            otherPlayer.AddGold(dmg_gold);
        }
        if (current_health <= 0 && !isdead)
        {
            Die();
        }
    }

    public void Die()
    {
        isdead = true;
        OnDie.Invoke();
        if (otherPlayer != null)
        {
            otherPlayer.AddGold(death_gold);
        }
        Destroy(gameObject);
    }

    void SetHealthColor()
    {
        foreach (MeshRenderer mr in healthMeshes)
        {
            Material m = mr.materials[dude_index];
                m.SetColor("_BaseColor", healthColorGradient.Evaluate((max_health - current_health) / max_health));
        }
    }
}
