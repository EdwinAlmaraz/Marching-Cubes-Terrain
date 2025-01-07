namespace RTL.Atlas
{
    public class BranchState : State
    {
        private Predicate condition;

        // requires a label for both true and false jumps
        public BranchState(Predicate condition)
        {
            this.condition = condition;
        }

        public override void Init()
        {
            int index = condition() ? 0 : 1;
            SelectBranch(index);
        }

        public override void Update(float timer) { }
    }
}