using System;

namespace Characters
{
    public class CharacterBehaviorIdle : CharacterBehavior
    {
        public Action ActionEnter;
        
        public override void Enter()
        {
            base.Enter();
            
            ActionEnter.Invoke();
        }
    }
}