using UnityEngine;

namespace RTL.Atlas
{
    public class SetKinematicState : State
    {
        private Rigidbody rigidbody;
        private bool enabled;

        public SetKinematicState(GameObject gameObject, bool enabled = true) : this(gameObject.GetComponent<Rigidbody>(), enabled) { }

        public SetKinematicState(Rigidbody rigidbody, bool enabled)
		{
			this.rigidbody = rigidbody;
			this.enabled = enabled;
		}

		public override void Init()
        {
            rigidbody.isKinematic = enabled;
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}