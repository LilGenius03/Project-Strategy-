using UnityEngine;

public class AttackSpawn : MonoBehaviour
{
    public Transform[] spawnPositions;

    private void OnEnable()
    {
        GameManager.OnRoundOver += PurgeAttackers;
    }

    private void OnDisable()
    {
        GameManager.OnRoundOver -= PurgeAttackers;
    }

    void PurgeAttackers(int winner)
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
