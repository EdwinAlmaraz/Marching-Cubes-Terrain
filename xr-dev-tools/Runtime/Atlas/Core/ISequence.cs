namespace RTL.Atlas
{
    public interface ISequence
    {
        public void AddState(State s, string[] jumplist = null);

        public void AddLabel(string label);

        public void AddJump(string label);

        public void FinishStateMachine();

        public void RunStateMachine();

        public bool IsFinished();

        public void PauseStateMachine();

        public void ResusmeStateMachine();

        public void StopStateMachine();

        public void Update();

        public void AddStateToBackground(State state);

        public void RemoveStateFromBackground(State state);

        public void OnTrigger(string id);

        // Branching Shortcuts
        public void IF(Predicate predicate);
        public void ELSE();
        public void ENDIF();
        public void WHILE(Predicate predicate);
        public void ENDWHILE();
        public void TRIGGERSWITCH(params string[] triggerIDs);
        public void ENDSWITCH();
        public void CASE();
        public void ENDCASE();
        public void RESETBLOCK(string triggerID, string deactivateTriggerID);
        public void ENDRESETBLOCK();
    }
}