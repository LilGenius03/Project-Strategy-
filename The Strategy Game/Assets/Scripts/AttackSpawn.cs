using UnityEngine;

public class AttackSpawn : MonoBehaviour
{
    public Transform[] spawnPositions;
    public Transform[] target_castle;

    bool is_combat;

    private void OnEnable()
    {
        GameManager.OnCombatBegin += IsCombat;
        GameManager.OnCombatOver += PurgeAttackers;
    }

    private void OnDisable()
    {
        GameManager.OnCombatBegin -= IsCombat;
        GameManager.OnCombatOver -= PurgeAttackers;
    }

    private void Update()
    {
        if(is_combat)
        {

        }
    }

    public void IsCombat()
    {
        is_combat = true;
    }

    void PurgeAttackers()
    {
        is_combat = false;
        Debug.Log("YOOOOO");
        foreach (Transform spawn in spawnPositions)
        {
            Debug.Log("DUDEEE");
            for (int i = spawn.childCount - 1; i > 0; i--)
            {
                Destroy(spawn.GetChild(i).gameObject);
                Debug.Log("DOPkdpoKDPOk");
            }
        }
        
    }

}
