using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int index;
    public PlayerController player_owner;

    public Dictionary<Vector2, grid_space> spaise = new Dictionary<Vector2, grid_space>();
    public List<grid_space> gridSpaces = new List<grid_space>();

    Vector2 prev_player_pos;

    private void Awake()
    {
        //player_owner = PlayerManager.instance.players[index].GetComponent<PlayerController>();
    }

    private void OnDrawGizmos()
    {
        foreach (grid_space space in gridSpaces)
        {
            switch (space.type_of_space)
            {
                case 0:
                    Gizmos.color = Color.gray;
                    break;
                case 1:
                    Gizmos.color = Color.blue;
                    break;
                case 2:
                    Gizmos.color = Color.red;
                    break;
            }
            Vector3 grid_poses = transform.position + new Vector3(space.position.x, 0f, space.position.y);
            Gizmos.DrawCube(grid_poses, new Vector3(1f, 0.0001f, 1f));
        }
    }

    public grid_space FindSpace(Vector2 pos)
    {
        return null;
    }

}

[System.Serializable]
public class grid_space
{
    public Vector2 position;
    public int type_of_space = 0; // 0 - non-playable, 1 - Tower, 2 - Obstacle
    public Tower current_tower;
}
