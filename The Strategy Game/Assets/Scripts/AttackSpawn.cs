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
        foreach (Transform spawn in spawnPositions)
        {
            for (int i = transform.childCount - 1; i > 1; i--)
            {
                Destroy(spawn.GetChild(i).gameObject);
            }
        }
        
    }

}
