using System;
using Network;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : View
{
    [SerializeField] private Button btnSearchMatch;

    public override void Initialize()
    {
        btnSearchMatch.onClick.AddListener(NetworkSend.ConnectedOrCreatedRoom);
        btnSearchMatch.onClick.AddListener(() => btnSearchMatch.gameObject.SetActive(false));
    }
}
