using System.Collections.Generic;

namespace RTL.Atlas
{
    public class TriggerState : State
    {
        private Dictionary<string, bool> triggers = new Dictionary<string, bool>();

        public TriggerState(params string[] triggerStrings)
        {
            for (int i = 0; i < triggerStrings.Length; ++i)
                triggers.Add(triggerStrings[i], false);
        }

        public override void Init()
        {
            foreach (string key in new List<string>(triggers.Keys))
                triggers[key] = false;
        }

        public override void Update(float timer) { }

        public override void Trigger(string id)
        {
            base.Trigger(id);

            if (triggers.ContainsKey(id))
                triggers[id] = true;

            bool advance = true;

            Dictionary<string, bool>.ValueCollection values = triggers.Values;
            foreach (KeyValuePair<string, bool> pair in triggers)
            {
                if (!pair.Value)
                    advance = false;
            }

            if (advance)
                SelectNextState();
        }
    }
}