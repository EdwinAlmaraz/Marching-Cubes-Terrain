namespace RTL.Atlas
{
    public class EndState : State
    {
        public override void Init() { container.StopStateMachine(); }

        public override void Update(float timer) { }
    }
}