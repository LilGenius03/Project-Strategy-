using UnityEngine;

public class AttackSpawn : MonoBehaviour
{
    public Transform[] spawnPositions;
    public Transform[] target_castle;

    private void OnEnable()
    {
        GameManager.OnCombatOver += PurgeAttackers;
    }

    private void OnDisable()
    {
        GameManager.OnCombatOver -= PurgeAttackers;
    }

    void PurgeAttackers()
    {
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
