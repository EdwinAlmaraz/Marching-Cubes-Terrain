using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTL.Atlas
{
	public class Sequence : ISequence
    {
        protected List<State> states;           //sequence of states ran sequentially
        protected List<State> backGroundStates; //list of states active in the background
        List<float> backgroundStateTimers;

        int currentState;                       //index of active state
        float timer;                            //time since start of current state

        public Sequence(State[] states = null)
        {
            this.states = new List<State>();
            backGroundStates = new List<State>();
            backgroundStateTimers = new List<float>();

            labels = new List<label>();
            jumps = new List<jumpList>();
            numStates = 0;

            if(states != null) {
                for (int i = 0; i < states.Length; i++) {
                    AddState(states[i]);
                }
            }
        }

        int numStates;

        public void AddState(State s, string[] jumplist = null)
        {
            int[] offset;
            if(jumplist == null)
            {
                offset = new int[] { 1 };
            }
            else
            {
                jumpList l = new jumpList();
                l.stateID = numStates;
                l.names = jumplist;
                jumps.Add(l);

                offset = new int[] {};
            }

            states.Add(s); //[numStates] = s;
            s.SetBranches(ResolveOffsets(numStates, offset));
            s.SetContainer(this);

            numStates++;
        }

        //resolves indicies from relative to absolute
        int[] ResolveOffsets(int num, int[] offsets)
        {
            int[] resolved = new int[offsets.Length];
            for (int i = 0; i < offsets.Length; i++)
            {
                resolved[i] = offsets[i] + num;
            }
            return resolved;
        }

        struct label {
            public int stateID;
            public string name;
            int s;
        }

        struct jumpList { 
            public int stateID;
            public string[] names;
            int s;
        };

        List<label> labels;
        List<jumpList> jumps;

        public void AddLabel(string label) 
        {
            label l = new label();// (numStates, );
            l.stateID = numStates;
            l.name = label;
            labels.Add(l);
        }
    
        public void AddJump(string label)
        {
            jumpList l = new jumpList();// (numStates, );
            l.stateID = numStates;
            l.names = new string[] { label };
            jumps.Add(l);

            AddState(new FillerState());
        }

        /// <summary>
        /// Finish Adding States
        /// </summary>
        public void FinishStateMachine() {
            for (int i = 0; i < jumps.Count; i++)
            {
                int[] nextTemp = new int[jumps[i].names.Length];
                for (int l = 0; l < jumps[i].names.Length; l++)
                {
                    int id = GetLabelID(jumps[i].names[l]);
                    if(id != -1)
                    {
                        nextTemp[l] = id;
                        //break;
                    }
                }
                states[jumps[i].stateID].SetBranches(nextTemp);
            }
        }

        int GetLabelID(string label)
        {
            for (int l = 0; l < labels.Count; l++)
            {
                if (labels[l].name == label) // jumps[i].name)
                {
                    return labels[l].stateID;
                }
            }
            return -1;
        }

        /////////////////////////////////////////////
        ///               Runing                  ///
        /////////////////////////////////////////////

        enum SequenceRunState
        {
            RUNNING,
            PAUSED,
            STOPPED
        } 
        SequenceRunState runstate = SequenceRunState.STOPPED;
    
        /// <summary>
        /// Begins Running State Machine
        /// </summary>
        public void RunStateMachine()
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i] != null)
                {
                    states[i].OnReset();
                }
            }

            runstate = SequenceRunState.RUNNING;
            SetState(0);
        }

        public bool  IsFinished()
        {
            return runstate == SequenceRunState.STOPPED;
        }

        public virtual void PauseStateMachine()
        {
            runstate = SequenceRunState.PAUSED;
            states[currentState].OnPause();
            for (int i = 0; i < backGroundStates.Count; i++)
            {
                backGroundStates[i].OnPause();
            }
        }

        public void ResusmeStateMachine()
        {
            runstate = SequenceRunState.RUNNING;
            states[currentState].OnResume();
            for (int i = 0; i < backGroundStates.Count; i++)
            {
                backGroundStates[i].OnResume();
            }

        }

        public virtual void StopStateMachine()
        {
            runstate = SequenceRunState.STOPPED;
            states[currentState].OnStop();
            for (int i = 0; i < backGroundStates.Count; i++)
            {
                backGroundStates[i].OnStop();
            }
            backGroundStates.Clear();
        }

        // Update is called once per frame
        public void Update()
        {
            if (runstate == SequenceRunState.RUNNING)
            {
                timer += Time.deltaTime;
                states[currentState].Update(timer);

                for (int i = 0; i < backGroundStates.Count; i++)
                {
                    backgroundStateTimers[i] += Time.deltaTime;
                    backGroundStates[i].Update(backgroundStateTimers[i]);
                }
            }
        }

        public void SetState(string id) {
            for (int i = 0; i < labels.Count; i++)
            {
                if(labels[i].name == id)
                {
                    SetState(labels[i].stateID);
                    return;
                }
            }
        }

        public void SetState(int id) {
            timer = 0;
            currentState = id;
            states[id].Init();
        }

        public void AddStateToBackground(State state)
        {
            backGroundStates.Add(state);
            backgroundStateTimers.Add(0.0f);
        }

        public void RemoveStateFromBackground(State state)
        {
            int id = backGroundStates.IndexOf(state);
            backGroundStates.RemoveAt(id);
            backgroundStateTimers.RemoveAt(id);
        }

        public virtual void OnTrigger(string id)
        {
            if (runstate == SequenceRunState.RUNNING)
            {
                states[currentState].Trigger(id);

                for (int i = 0; i < backGroundStates.Count; ++i)
                    backGroundStates[i].Trigger(id);
            }
        }

        #region Branching Shortcuts
        private Stack<string> labelStack = new Stack<string>();
        private Stack<string> switchEnd = new Stack<string>();
        private uint labelCounter = 0;

        public void IF(Predicate predicate)
        {
            string branch0, branch1;

            labelStack.Push(createLabel());
            branch1 = labelStack.Peek();
            labelStack.Push(createLabel());
            branch0 = labelStack.Peek();

            AddState(new BranchState(predicate), new string[] { branch0, branch1 });
            AddLabel(labelStack.Pop());
            // Code
        }

        public void ELSE()
        {
            string elseLabel = labelStack.Pop();

            labelStack.Push(createLabel());
            AddJump(labelStack.Peek());

            AddLabel(elseLabel);
            // Code
        }

        public void ENDIF()
        {
            AddLabel(labelStack.Pop());
        }

        public void WHILE(Predicate predicate)
        {
            string preLabel, startLabel, endLabel;

            labelStack.Push(createLabel());
            endLabel = labelStack.Peek();

            labelStack.Push(createLabel());
            preLabel = labelStack.Peek();

            labelStack.Push(createLabel());
            startLabel = labelStack.Pop();

            AddLabel(preLabel);
            AddState(new BranchState(predicate), new string[] { startLabel, endLabel });
            AddLabel(startLabel);
            // Code
        }

        public void ENDWHILE()
        {
            AddJump(labelStack.Pop());  // Jump To Pre
            AddLabel(labelStack.Pop()); // label for false condition
        }

        public void TRIGGERSWITCH(params string[] triggerIDs)
        {
            if (triggerIDs == null || triggerIDs.Length == 0)
                Debug.LogError("No Triggers Provided");

            // Label at end of switch statement to escape following cases
            switchEnd.Push(createLabel());

            List<string> jumpLabels = new List<string>();

            for (int i = 0; i < triggerIDs.Length; ++i)
            {
                labelStack.Push(createLabel());
                jumpLabels.Add(labelStack.Peek());
            }

            jumpLabels.Reverse();

            AddState(new SwitchState(triggerIDs), jumpLabels.ToArray());

        }

        public void ENDSWITCH()
        {
            AddLabel(switchEnd.Pop());
        }

        public void CASE()
        {
            AddLabel(labelStack.Pop());
            // Code
        }

        public void ENDCASE()
        {
            AddJump(switchEnd.Peek());
        }

        public void RESETBLOCK(string triggerID, string deactivateTriggerID)
        {
            string pre = createLabel();
            string normal = createLabel();
            string fail = createLabel();

            labelStack.Push(normal);
            labelStack.Push(pre);

            AddLabel(pre);
            AddState(new BackgroundTriggerState(triggerID, deactivateTriggerID), new string[] { normal, fail });
            AddLabel(fail);
            // Event Code
        }

        // End of Code Section, doesn't disable background trigger
        public void ENDRESETBLOCK()
        {
            // Jump to pre
            AddJump(labelStack.Pop());

            // Normal
            AddLabel(labelStack.Pop());
        }

        private string createLabel()
        {
            string ret = "___" + labelCounter.ToString() + "___";
            ++labelCounter;
            return ret;
        }
        #endregion

    }//end Sequence
}