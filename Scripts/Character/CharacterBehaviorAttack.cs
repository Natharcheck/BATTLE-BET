using System;

namespace Characters
{
    public class CharacterBehaviorAttack : CharacterBehavior
    {
        public Action ActionEnter;
        
        public override void Enter()
        {
            base.Enter();
            
            ActionEnter.Invoke();
        }
    }
}