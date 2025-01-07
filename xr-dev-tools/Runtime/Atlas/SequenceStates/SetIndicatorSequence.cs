using UnityEngine;
using UnityEngine.Events;

namespace RTL.Atlas
{
    public class SetIndicatorSequence : SequenceState
    {
        Indicator indicator;
        string message;
        Transform target;
        Vector3 targetOffset;
        bool drawArrow;
        bool showbtn;
        string btnText;

        // public SetIndicatorSequence(GameObject indicator, string message, Transform target, Vector3 targetOffset, bool drawArrow = true)
        // {
        //     this.indicator = indicator.GetComponent<Indicator>();
        //     this.message = message;
        //     this.target = target;
        //     this.drawArrow = drawArrow;
        //     this.targetOffset = targetOffset;

        //     addStates();
        // }
        public SetIndicatorSequence(string message, Transform target, Vector3 targetOffset, bool drawArrow = true, bool showbtn = false, string btnText = "Okay") : base()
        {
            indicator = Indicator.instance;
            this.message = message;
            this.target = target;
            this.drawArrow = drawArrow;
            this.targetOffset = targetOffset;
            this.showbtn = showbtn;
            this.btnText = btnText;
            AddStates();
        }
        // public SetIndicatorSequence(GameObject indicator, string message, GameObject target, Vector3 targetOffset, bool drawArrow = true) 
        // : this(indicator, message, target.transform, targetOffset, drawArrow) { }

        // public SetIndicatorSequence(GameObject indicator, string message, Transform target, bool drawArrow = true) 
        // : this(indicator, message, target, new Vector3(0, 0.35f, 0), drawArrow) { }

        // public SetIndicatorSequence(GameObject indicator, string message, GameObject target, bool drawArrow = true) 
        // : this(indicator, message, target.transform, drawArrow) { }

        public SetIndicatorSequence(string message, GameObject target, Vector3 targetOffset, bool drawArrow = true, bool showbtn = false, string btnText = "Okay")
        : this(message, target.transform, targetOffset, drawArrow, showbtn, btnText) { }

        public SetIndicatorSequence(string message, Transform target, bool drawArrow = true, bool showbtn = false, string btnText = "Okay")
        : this(message, target, Vector3.zero, drawArrow, showbtn, btnText) { }

        public SetIndicatorSequence(string message, GameObject target, bool drawArrow = true, bool showbtn = false, string btnText = "Okay")
        : this(message, target.transform, new Vector3(0, 0.85f, 0), drawArrow, showbtn, btnText) { }

        public override void Init()
        {
            base.Init();
            if (showbtn)
                indicator.Register(this);
        }
        public override void Trigger(string id)
        {
            base.Trigger(id);
            if (showbtn)
                indicator.Deregister(this);
        }

        protected void AddStates()
        {
            AddState(new SetPositionState(indicator.transform, target, targetOffset));
            AddState(new SetTextState(indicator.Instructions, message));
            AddState(new SetActiveState(indicator, true));

            if (drawArrow)
                AddState(new SetActiveState(indicator.DownArrow));
            else
                AddState(new SetActiveState(indicator.DownArrow, false));
            if (showbtn)
            {
                AddState(new SetTextState(indicator.BtnText, btnText));
                AddState(new SetActiveState(indicator.btn));
                AddState(new TriggerState("indicatorbtnpressed"));
            }
            else
            {
                AddState(new SetActiveState(indicator.btn, false));
            }

            AddState(new EndState());
            FinishStateMachine();
        }
    }
}