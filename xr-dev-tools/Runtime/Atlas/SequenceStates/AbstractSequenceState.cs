namespace RTL.Atlas
{
    public abstract class AbstractSequenceState : State, ISequence
    {
        private Sequence sequence = new Sequence();

        public override void Trigger(string id) { OnTrigger(id); }

        public override void OnPause() { PauseStateMachine(); }
        public override void OnResume() { ResusmeStateMachine(); }
        public override void OnStop() { StopStateMachine(); }

        public override void Update(float timer) { Update(); }

        #region ISequence Implementation
        public void AddState(State s, string[] jumplist = null) { sequence.AddState(s, jumplist); }
        public void AddLabel(string label) { sequence.AddLabel(label); }
        public void AddJump(string label) { sequence.AddJump(label); }
        public void FinishStateMachine() { sequence.FinishStateMachine(); }
        public virtual void RunStateMachine() { sequence.RunStateMachine(); }
        public virtual void PauseStateMachine() { sequence.PauseStateMachine(); }
        public virtual void ResusmeStateMachine() { sequence.ResusmeStateMachine(); }
        public virtual void StopStateMachine() { sequence.StopStateMachine(); }

        public void Update() { sequence.Update(); }
        public virtual void OnTrigger(string id) { sequence.OnTrigger(id); }
        public bool IsFinished() { return sequence.IsFinished(); }

        public void AddStateToBackground(State state) { sequence.AddStateToBackground(state); }
        public void RemoveStateFromBackground(State state) { sequence.RemoveStateFromBackground(state); }

        public void IF(Predicate predicate) { sequence.IF(predicate); }
        public void ELSE() { sequence.ELSE(); }
        public void ENDIF() { sequence.ENDIF(); }
        public void WHILE(Predicate predicate) { sequence.WHILE(predicate); }
        public void ENDWHILE() { sequence.ENDWHILE(); }
        public void TRIGGERSWITCH(params string[] triggerIDs) { sequence.TRIGGERSWITCH(triggerIDs); }
        public void ENDSWITCH() { sequence.ENDSWITCH(); }
        public void CASE() { sequence.CASE(); }
        public void ENDCASE() { sequence.ENDCASE(); }
        public void RESETBLOCK(string triggerID, string deactivateTriggerID) { sequence.RESETBLOCK(triggerID, deactivateTriggerID); }
        public void ENDRESETBLOCK() { sequence.ENDRESETBLOCK(); }

        #endregion
    }
}