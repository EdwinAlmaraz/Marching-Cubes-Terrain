using UnityEngine;

namespace RTL.Atlas
{
    public class SetRotationState : State
    {
        private Transform transform;
        private Transform destination;
        private Vector3 offset;

        public SetRotationState(Transform transform, Transform destination, Vector3 offset = default(Vector3))
        {
            this.transform = transform;
            this.destination = destination;
            this.offset = offset;
        }

        public SetRotationState(GameObject gameObject, Transform destination, Vector3 offset = default(Vector3)) : this(gameObject.transform, destination, offset) { }
        public SetRotationState(Component component, Transform destination, Vector3 offset = default(Vector3)) : this(component.transform, destination, offset) { }

        public override void Init()
        {
            transform.rotation = destination.rotation;
            transform.rotation *= Quaternion.Euler(offset);
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}