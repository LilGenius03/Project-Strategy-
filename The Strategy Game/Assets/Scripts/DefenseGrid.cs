using System;
using System.Collections.Generic;
using UnityEngine;

public class DefenseGrid : MonoBehaviour
{
    public int index;
    public PlayerController player_owner;
    public Vector3 player_start_pos;

    public List<grid_space> gridSpaces = new List<grid_space>();

    public float playable_space_x_min, playable_space_x_max;
    public float playable_space_y_min, playable_space_y_max;


    private void Awake()
    {
        //player_owner = PlayerManager.instance.players[index].GetComponent<PlayerController>();
    }

    public grid_space FindSpace(Vector3 pos)
    {
        foreach (grid_space space in gridSpaces)
        {
            if (space.transform.position == pos)
                return space;
        }
        return null;
    }

    public void PlaceTower(grid_space space, GameObject tower)
    {
        if(space.type_of_space == 1 && space.current_tower == null)
        {
            GameObject newTower = Instantiate(tower, space.transform.position, Quaternion.identity, transform);
            space.current_tower = newTower.GetComponent<Tower>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + player_start_pos, 0.1f);
    }
}
