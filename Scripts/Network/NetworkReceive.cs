using System;
using Riptide;
using UnityEngine;

namespace Network
{
    public enum ServerPackets : ushort
    {
        Verification = 1,
        LoadDataClient,

        StartLoadScene,
        StartMatch,

        CreatePlayer,
        CreateAbility,
        BehaviorPlayer,
        MovementPayer,

        CreateEnemies,
        CreateAbilityEnemy,
        BehaviorEnemy,
        MovementEnemy,
        
        UnitHealth,
        
        RoomTime,
        PositionIslands,
    }

    public static class NetworkReceive //Client
    {
        [MessageHandler((ushort)ServerPackets.Verification)]
        private static void Verification(Message message)
        {
            ViewManager.ShowMessage("Enter", MessagePanel.Type.Successful);
            ViewManager.Show<MenuView>();
        }
        
        [MessageHandler((ushort)ServerPackets.LoadDataClient)]
        private static void LoadDataClient(Message message)
        {
            ViewManager.ShowMessage("Load data", MessagePanel.Type.Successful);
            
            Profile.Username = message.GetString();
            Profile.Gold = message.GetInt();
            Profile.Crystals = message.GetInt();
            
            ViewManager.InitializeProfile();
        }
        
        [MessageHandler((ushort)ServerPackets.StartLoadScene)]
        private static void StartLoadScene(Message message)
        {
            var loadSceneView = ViewManager.GetView<LoadSceneView>();
                loadSceneView.SceneName = message.GetString();
                loadSceneView.Show();
        }
        
        #region Room
        
        [MessageHandler((ushort)ServerPackets.StartMatch)]
        private static void StartMatch(Message message)
        {
            
        }
        
        [MessageHandler((ushort)ServerPackets.CreatePlayer)]
        private static void CreatePlayer(Message message)
        {
            var roomManager = RoomManager.Instance;
            var connectionId = message.GetInt();
            var currentCharacter = message.GetInt();

            roomManager.SpawnPlayer(connectionId, currentCharacter);
        }
        
        [MessageHandler((ushort)ServerPackets.BehaviorPlayer)]
        private static void BehaviorPlayer(Message message)
        {
            var connectionId = message.GetInt();
            var behaviorId = message.GetInt();
            var player =  RoomManager.Instance.GetPlayer(connectionId);

            player.SetBehaviorReceiveId(behaviorId);
        }
        
        [MessageHandler((ushort)ServerPackets.MovementPayer)]
        private static void MovementPayer(Message message)
        {
            var connectionId = message.GetInt();
            var angle = message.GetInt();
            var player =  RoomManager.Instance.GetPlayer(connectionId);
            
            var positionX = message.GetFloat();
            var positionZ = message.GetFloat();
            
            player.transform.position = new Vector3(positionX, 0, positionZ);
            player.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        
        [MessageHandler((ushort)ServerPackets.CreateEnemies)]
        private static void CreateEnemies(Message message)
        {
            var roomManager = RoomManager.Instance;
            var count = message.GetInt();

            for (var i = 0; i < count; i++)
            {
                var networkId = message.GetUShort();
                var internalId = message.GetInt();
                
                roomManager.AddEnemy(networkId, internalId);
            }
        }
        
        [MessageHandler((ushort)ServerPackets.BehaviorEnemy)]
        private static void BehaviorEnemy(Message message)
        {
            var connectionId = message.GetInt();
            var behaviorId = message.GetInt();
            var enemy =  RoomManager.Instance.GetEnemy(connectionId);

            enemy.SetBehaviorReceiveId(behaviorId);
        }
        
        [MessageHandler((ushort)ServerPackets.MovementEnemy)]
        private static void MovementEnemy(Message message)
        {
            var connectionId = message.GetInt();
            var angle = message.GetInt();
            var enemy =  RoomManager.Instance.GetEnemy(connectionId);
            
            var positionX = message.GetFloat();
            var positionZ = message.GetFloat();
            
            enemy.transform.position = new Vector3(positionX, 0, positionZ);
            enemy.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        
        [MessageHandler((ushort)ServerPackets.UnitHealth)]
        private static void UnitHealth(Message message)
        {
            var unitType = message.GetInt();
            var connectionCount = message.GetInt();

            for (var i = 0; i < connectionCount; i++)
            {
                var connectionId = message.GetUShort();
                var health = message.GetInt();

                switch (unitType)
                {
                    case 0:
                        var player = RoomManager.Instance.GetPlayer(connectionId); 
                        player.healthView.Health = health;
                        break;
                    case 1: 
                        var enemy = RoomManager.Instance.GetEnemy(connectionId); 
                        enemy.healthView.Health = health;
                        break;
                }   
            }
        }
        
        [MessageHandler((ushort)ServerPackets.PositionIslands)]
        private static void PositionIslands(Message message)
        {
            var connectionId = message.GetInt();
            var isLand =  RoomManager.Instance.GetIsland(connectionId);
            
            var positionX = message.GetFloat();
            var positionZ = message.GetFloat();

            isLand.transform.position = new Vector3(positionX, 0, positionZ);
        }
        
        [MessageHandler((ushort)ServerPackets.RoomTime)]
        private static void RoomTime(Message message)
        {
            var time = message.GetInt();
            
            ViewManager.GetView<RoundView>().InitTime(time);
        }
        
        #endregion
    }
}