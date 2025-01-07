namespace RTL.Atlas
{
    public abstract class State
    {
        public Sequence container { get; protected set; }
        private int[] nextState;
        bool isInBackground;

        public abstract void Init();
        public abstract void Update(float timer);
        public virtual void Trigger(string id) { }

        public virtual void OnPause() { }
        public virtual void OnResume() { }
        public virtual void OnStop() { }
        public virtual void OnReset() { }

        protected void SelectNextState() {
            SelectBranch(0);
        }
   
        protected void SelectBranch(int branch) {
            if (isInBackground)
                RemoveStateFromBackground();

            container.SetState(nextState[branch]);
        }

        public void SetBranches(int[] next) {
            nextState = next;
        }

        public void SetContainer(Sequence controller) {
            container = controller;
        }

        public void AddStateToBackground()
        {
            container.AddStateToBackground(this);
            isInBackground = true;
        }

        public void RemoveStateFromBackground()
        {
            container.RemoveStateFromBackground(this);
            isInBackground = false;
        }
    }
}