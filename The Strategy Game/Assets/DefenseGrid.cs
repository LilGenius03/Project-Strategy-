using System;
using System.Collections.Generic;
using UnityEngine;

public class DefenseGrid : MonoBehaviour
{
    public int index;
    public PlayerController player_owner;

    public List<grid_space> gridSpaces = new List<grid_space>();

    public float playable_space_x, playable_space_y;

    [SerializeField] private GameObject debug_tower;

    private void Awake()
    {
        //player_owner = PlayerManager.instance.players[index].GetComponent<PlayerController>();
    }

    public grid_space FindSpace(Vector3 pos)
    {
        foreach (grid_space space in gridSpaces)
        {
            if (space.transform.localPosition == pos)
                return space;
        }
        return null;
    }

    public void PlaceTower(grid_space space)
    {
        if(space.type_of_space == 1 && space.current_tower == null)
        {
            GameObject newTower = Instantiate(debug_tower, space.transform.position, Quaternion.identity, transform);
            space.current_tower = newTower.GetComponent<Tower>();
        }
    }

/*    private void OnDrawGizmos()
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
    } */
}
