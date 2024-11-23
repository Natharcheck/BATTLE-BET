using Characters;
using Characters.Example;
using Network;
using UnityEngine;

public class JoystickMovement : JoystickHandler
{
    public LocalPlayer targetPlayer;
    
    public override void Initialize()
    {
        base.Initialize();
        
        BeginDrag.AddListener(Begin);
        Drag.AddListener(Updated);
        EndDrag.AddListener(End);
    }
    
    private void Begin()
    {
        NetworkSend.BehaviorPlayer((int)Character.Behavior.Move);
    }
    
    private void Updated()
    {
        targetPlayer.Angle = (int)Angle;
    }

    private void End()
    {
        NetworkSend.BehaviorPlayer((int)Character.Behavior.Idle);
    }
}