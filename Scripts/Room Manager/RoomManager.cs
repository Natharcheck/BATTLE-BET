using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;
using Characters.Example;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    public LocalPlayer MainPlayer { get; private set; }
    [SerializeField] private LocalPlayer mainPlayerPrefab;
    [SerializeField] private Player playerPrefab;
    [Space]
    [SerializeField] private List<Character> players;
    [SerializeField] private List<Enemy> enemies;
    [Space]
    [SerializeField] private List<Island> islands;
    [Space]
    [SerializeField] private PoolEnemies poolEnemies;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private JoystickMovement joystickMovement;

    private void Awake() => Instance = this;

    public void SpawnPlayer(int connectionId, int currentCharacter)
    {
        if (connectionId == NetworkManager.Singleton.Client.Id)
        {
            var player = Instantiate(mainPlayerPrefab, null);
            player.ConnectionId = connectionId;
                
            players.Add(player);
            
            MainPlayer = player;
            cameraController.target = player.transform;
            joystickMovement.targetPlayer = player;
        }
        else
        {
            var player = Instantiate(playerPrefab, null);
            
            player.ConnectionId = connectionId;
            
            players.Add(player);
        }
    }

    public Character GetPlayer(int connectionId)
    {
        foreach (var player in players)
        {
            if (player.ConnectionId == connectionId)
                return player;
        }

        return null;
    }

    public void AddEnemy(int networkId, int internalId)
    {
        var enemy = poolEnemies.CreateEnemy(networkId, internalId);
        
        enemies.Add(enemy);
    }

    public Enemy GetEnemy(int networkId)
    {
        foreach (var enemy in enemies)
        {
            if (enemy.ConnectionId == networkId)
                return enemy;
        }

        return null;
    }
    
    public Island GetIsland(int networkId)
    {
        foreach (var island in islands)
        {
            if (island.id == networkId)
                return island;
        }

        return null;
    }
}