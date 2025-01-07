namespace RTL.Atlas
{
    public class ActionState : State
    {
        private Action onInitialize;

        public ActionState(Action onInitialize)
        {
            this.onInitialize = onInitialize;
        }

        public override void Init()
        {
            onInitialize();
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}