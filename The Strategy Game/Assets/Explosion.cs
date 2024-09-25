using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float damage, growSpeed, explosionTime;

    private void Start()
    {
        Destroy(gameObject, explosionTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        damage -= Time.deltaTime * 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("attack_unit"))
        {
            other.transform.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
