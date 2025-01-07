using UnityEngine;

namespace RTL.Atlas
{
    public class LogState : State
    {
        string message;
        
        public LogState(string message) { this.message = message; }

        public override void Init()
        {
            Debug.Log(message);
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}