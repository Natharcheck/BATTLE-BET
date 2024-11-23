using Riptide;
using UnityEngine;

namespace Network
{
    public enum ClientPackets : ushort
    {
        Authorization = 1,
        Registration,
		
        ConnectedOrCreatedRoom,
        MapLoadingSuccessful,
        
        CreateAbility,
        BehaviorPlayer,
        MovementPayer,
    }
    
    public static class NetworkSend
    {
        public static void Authorization(string username, string password)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientPackets.Authorization);
                message.AddString(username);
                message.AddString(password);

            NetworkManager.Singleton.Client.Send(message);
        }
    
        public static void Registration(string username, string password)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientPackets.Registration);
                message.AddString(username);
                message.AddString(password);
            
            NetworkManager.Singleton.Client.Send(message);
        }
        
        public static void ConnectedOrCreatedRoom()
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientPackets.ConnectedOrCreatedRoom);
            //Отправка режима игры
            ViewManager.ShowMessage("Find Match ", MessagePanel.Type.Successful);
            NetworkManager.Singleton.Client.Send(message);
        }
        
        public static void MapLoadingSuccessful()
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientPackets.MapLoadingSuccessful);
            
            NetworkManager.Singleton.Client.Send(message);
        }
        
        public static void BehaviorPlayer(int behaviorId)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientPackets.BehaviorPlayer);

            message.AddInt(behaviorId);
            
            NetworkManager.Singleton.Client.Send(message);
        }
        
        public static void MovementPlayer(int angle)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientPackets.MovementPayer);

            message.AddInt(angle);
            
            NetworkManager.Singleton.Client.Send(message);
        }
    }
}