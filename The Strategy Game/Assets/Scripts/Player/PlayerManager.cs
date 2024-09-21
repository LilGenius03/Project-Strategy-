using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public delegate void PlayerHasJoined(int playerID);
    public static event PlayerHasJoined OnPlayerHasJoined;

    public List<PlayerInput> players = new List<PlayerInput>();
    public DefenseGrid[] player_grids;
    public AttackSpawn[] player_attack_spawns;

    private PlayerInputManager playerInputManager;

    public static PlayerManager instance;
    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        if (instance != null)
        {
            Debug.LogWarning("Error more than one " + name + " component found");
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
        OnPlayerHasJoined += PlayerJoined;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
        OnPlayerHasJoined -= PlayerJoined;
    }

    public void EnableJoining()
    {
        playerInputManager.EnableJoining();
    }

    public void DisableJoining()
    {
        playerInputManager.DisableJoining();
    }

    public void AddPlayer(PlayerInput player)
    {
        player.transform.parent = transform;

        players.Add(player);
        player.name = "Player_" + players.Count;

        PlayerController controller = player.GetComponent<PlayerController>();
        controller.id = (short) players.Count;
        controller.defenseGrid = player_grids[players.Count - 1];
        controller.attack_spawn = player_attack_spawns[players.Count - 1];
        OnPlayerHasJoined.Invoke(players.Count);
    }

    public void ClearPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Destroy(players[i]);
        }
        players.Clear();
        playerInputManager.DisableJoining();
    }

    public void PlayerJoined(int id)
    {
        Debug.Log("Player " + id + " has joined");
    }
}
