namespace RTL.Atlas
{
    public class WaitForConditionState : State
    {
        private Predicate condition;

        public WaitForConditionState(Predicate condition)
        {
            this.condition = condition;
        }

        public override void Init()
        {
            CheckCondition();
        }

        public override void Update(float timer)
        {
            CheckCondition();
        }

        protected virtual void CheckCondition()
        {
            if (condition())
            {
                SelectNextState();
            }
        }
    }
}