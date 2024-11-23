using Characters;
using Network;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesView : View
{
    [SerializeField] private Button btnAttack;
    
    public override void Initialize()
    {
        btnAttack.onClick.AddListener(() => NetworkSend.BehaviorPlayer((int)Character.Behavior.Attack));
    }
}