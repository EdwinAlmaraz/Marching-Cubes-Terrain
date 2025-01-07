using UnityEngine;

namespace RTL.Atlas
{
    public class SetPositionState : State
    {
        private Transform transform;
        private Transform destination;
        private Vector3 offset;

        public SetPositionState(Transform transform, Transform destination, Vector3 offset = default(Vector3))
        {
            this.transform = transform;
            this.destination = destination;
            this.offset = offset;
        }

        public SetPositionState(GameObject gameObject, Transform destination, Vector3 offset = default(Vector3)) : this(gameObject.transform, destination, offset) { }
        public SetPositionState(Component component, Transform destination, Vector3 offset = default(Vector3)) : this(component.transform, destination, offset) { }

        public override void Init()
        {
            transform.position = destination.position + offset;
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}