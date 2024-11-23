using Network;

namespace Characters.Example
{
    public class LocalPlayer : Character
    {
        public override void Move()
        {
            base.Move();
            
            NetworkSend.MovementPlayer(Angle);
        }
    }
}