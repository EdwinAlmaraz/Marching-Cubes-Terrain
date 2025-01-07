using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTL.Atlas
{
    public class SequenceController : MonoBehaviour, ISequence
    {
        Sequence sequence;

        /////////////////////////////////////////////
        ///               Setup                   ///
        /////////////////////////////////////////////

        /// <summary>
        /// Call before adding States
        /// </summary>
        public void InitStateMachine() { sequence = new Sequence(); } 

        public void AddState(State s, string[] jumplist = null) { sequence.AddState(s, jumplist); }
        public void AddLabel(string label) { sequence.AddLabel(label); }
        public void AddJump(string label) { sequence.AddJump(label); }

        /// <summary>
        /// Finish Adding States
        /// </summary>
        public void FinishStateMachine() { sequence.FinishStateMachine(); } 

        /////////////////////////////////////////////
        ///               Runing                  ///
        /////////////////////////////////////////////

        /// <summary>
        /// Begins Running State Machine
        /// </summary>
        public virtual void RunStateMachine() { sequence.RunStateMachine(); }
        public virtual void PauseStateMachine() { sequence.PauseStateMachine(); }
        public virtual void ResusmeStateMachine() { sequence.ResusmeStateMachine(); }
        public virtual void StopStateMachine() { sequence.StopStateMachine(); }


        public virtual void Update() { sequence.Update(); }
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
    }
}