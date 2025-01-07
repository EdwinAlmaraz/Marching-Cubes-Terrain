namespace RTL.Atlas
{
    public class SequenceState : AbstractSequenceState
    {
        public override void Init()
        {
            RunStateMachine();
        }

        public override void Update(float timer)
        {
            base.Update(timer);

            if (IsFinished())
            {
                SelectNextState();
            }
        }
    }
}