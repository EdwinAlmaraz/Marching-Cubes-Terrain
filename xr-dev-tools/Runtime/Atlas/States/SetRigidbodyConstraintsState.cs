using UnityEngine;

namespace RTL.Atlas
{
    public class SetRigidbodyConstraintsState : State
    {
        private Rigidbody rigidbody;
        private RigidbodyConstraints constraints;

        public SetRigidbodyConstraintsState(GameObject gameObject, RigidbodyConstraints constraints) : this(gameObject.GetComponent<Rigidbody>(), constraints) { }

        public SetRigidbodyConstraintsState(Rigidbody rigidbody, RigidbodyConstraints constraints)
		{
			this.rigidbody = rigidbody;
			this.constraints = constraints;
		}

		public override void Init()
        {
            rigidbody.constraints = constraints;
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}