using UnityEngine;
using UnityEngine.UI;

public class ReconnectionView : View
{
    [SerializeField] private Button btnReconnection;

    public override void Initialize()
    {
        btnReconnection.onClick.AddListener(NetworkManager.Singleton.ReConnect);
        btnReconnection.onClick.AddListener(Hide);
    }
}