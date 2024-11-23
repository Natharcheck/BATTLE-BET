using System;
using Riptide;
using Riptide.Utils;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    
    public Riptide.Client Client { get; set; }
    
    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    
    private void Awake() 
    {
        Singleton = this; 
        DontDestroyOnLoad(gameObject);
        
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
    
        Client = new Riptide.Client();
        
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += PlayerLeft;
        Client.Disconnected += DidDisconnect;
        
        Connect();
    }
    
    private void FixedUpdate()
    {
        Client.Update();
    }
    
    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }
    
    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
    }
    
    public void ReConnect()
    {
        if(Client.IsConnected == false)
            Client.Connect($"{ip}:{port}");
    }
    
    private void DidConnect(object sender, EventArgs e)
    {
        ViewManager.ShowMessage("Connected", MessagePanel.Type.Successful);
    }
    
    private void FailedToConnect(object sender, EventArgs e)
    {
        ViewManager.ShowMessage("Connected", MessagePanel.Type.Failed);
        ViewManager.ShowOnly<ReconnectionView>();
    }
    
    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        ViewManager.ShowMessage("Ops, ", MessagePanel.Type.Failed);
        ViewManager.ShowOnly<ReconnectionView>();
    }
    
    private void DidDisconnect(object sender, EventArgs e)
    {
        ViewManager.ShowMessage("Disconnected", MessagePanel.Type.Failed);
        ViewManager.ShowOnly<ReconnectionView>();
    }
}