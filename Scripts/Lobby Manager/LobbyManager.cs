using System;
using System.Collections;
using Network;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Authorization(string username, string password)
    {
        if(StringReady(username, "Username") == false)
            return;
        
        if(StringReady(password, "Password") == false)
            return;
        
        NetworkSend.Authorization(username, password);
    }
    
    public void Registration(string username, string password, string repeatPassword)
    {
        if(StringReady(username, "Username") == false)
            return;
        
        if(StringReady(password, "Password") == false)
            return;

        if (password != repeatPassword)
        {
            ViewManager.ShowMessage("Repeat password isn't correct", MessagePanel.Type.Failed);
            return;
        }
        
        NetworkSend.Registration(username, password);
    }
    
    private bool StringReady(string line, string message)
    {
        var length = 5;
        
        if (line == string.Empty)
        {
            ViewManager.ShowMessage($"{message} is empty", MessagePanel.Type.Failed);
            return false;
        }
        if (line.Length < length)
        {
            ViewManager.ShowMessage($"{message} less than {length} characters", MessagePanel.Type.Failed);
            return false;
        }

        return true;
    }
}