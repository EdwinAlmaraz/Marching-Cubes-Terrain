using UnityEngine;

namespace RTL.Atlas
{
    public class UseGravityState : State
    {
        private Rigidbody rigidbody;
        private bool enabled;

        public UseGravityState(GameObject gameObject, bool enabled = true) : this(gameObject.GetComponent<Rigidbody>(), enabled) { }

        public UseGravityState(Rigidbody rigidbody, bool enabled = true)
		{
			this.rigidbody = rigidbody;
			this.enabled = enabled;
		}

		public override void Init()
        {
            rigidbody.useGravity = enabled;
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}