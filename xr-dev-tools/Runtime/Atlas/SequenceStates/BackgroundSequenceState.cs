namespace RTL.Atlas
{
    public abstract class BackgroundSequenceState : AbstractSequenceState
    {
        public override void Init()
        {
            SelectNextState();
            container.AddStateToBackground(this);
            RunStateMachine();
        }

        public override void Update(float timer)
        {
            base.Update(timer);

            if (IsFinished())
            {
                container.RemoveStateFromBackground(this);
            }
        }
    }
}