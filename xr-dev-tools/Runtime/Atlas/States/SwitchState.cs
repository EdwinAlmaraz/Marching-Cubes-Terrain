using System.Collections.Generic;

namespace RTL.Atlas
{
    public class SwitchState : State
    {
        private List<string> triggers;

        public SwitchState(params string[] triggers)
        {
            this.triggers = new List<string>();

            for (int i = 0; i < triggers.Length; ++i)
                this.triggers.Add(triggers[i]);
        }

        public override void Init() { }

        public override void Update(float timer) { }

        public override void Trigger(string id)
        {
            base.Trigger(id);

            int index = triggers.IndexOf(id);

            if (index != -1)
                SelectBranch(index);
        }
    }
}