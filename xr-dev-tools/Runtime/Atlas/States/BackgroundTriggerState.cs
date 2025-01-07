namespace RTL.Atlas
{
    public class BackgroundTriggerState : State
    {
        string activateTriggerID, deactivateTriggerID;

        public BackgroundTriggerState(string activateTriggerID, string deactivateTriggerID)
        {
            this.activateTriggerID = activateTriggerID;
            this.deactivateTriggerID = deactivateTriggerID;
        }

        public override void Init()
        {
            // Immediately move on to branch specified by first label
            SelectBranch(0);
            AddStateToBackground();
        }

        public override void Trigger(string id)
        {
            base.Trigger(id);

            // selectBranch removes state from background automatically
            if (id == activateTriggerID)
            {
                SelectBranch(1);
            }

            if (id == deactivateTriggerID)
                RemoveStateFromBackground();
        }
        public override void Update(float timer) { }
    }
}