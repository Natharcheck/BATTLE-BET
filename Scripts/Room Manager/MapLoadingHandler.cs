using Network;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoadingHandler : MonoBehaviour
{
    private bool _isGrant = true;
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (_isGrant)
        {
            NetworkSend.MapLoadingSuccessful();
            _isGrant = false;
        }
    }
}