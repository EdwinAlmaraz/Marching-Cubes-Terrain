using UnityEngine;

namespace RTL.Atlas
{
    public class SetParentTransformState : State
    {
        Transform target;
        Transform newParent;

        public SetParentTransformState(Transform target, Transform newParent) : base()
        {
            this.target = target;
            this.newParent = newParent;
        }

        public SetParentTransformState(GameObject target, Transform newParent) : this(target.transform, newParent) { }
        public SetParentTransformState(Transform target, GameObject newParent) : this(target, newParent.transform) { }
        public SetParentTransformState(GameObject target, GameObject newParent) : this(target.transform, newParent.transform) { }

        public override void Init()
        {
            target.parent = newParent;
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}