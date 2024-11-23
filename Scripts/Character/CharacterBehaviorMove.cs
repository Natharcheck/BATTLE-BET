using System;
using Network;

namespace Characters
{
    public class CharacterBehaviorMove : CharacterBehavior
    {
        public Action ActionUpdated;
        public override void Updated()
        {
            base.Updated();
            ActionUpdated.Invoke();
        }
    }
}