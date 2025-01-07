using UnityEngine;

namespace RTL.Atlas
{
    public class SetActiveState : State
    {
        private GameObject gameObject;
        private bool active;

        public SetActiveState(GameObject gameObject, bool active = true)
        {
            this.gameObject = gameObject;
            this.active = active;
        }

        public SetActiveState(Transform transform, bool active = true) : this(transform.gameObject, active) { }
        public SetActiveState(Component component, bool active = true) : this(component.gameObject, active) { }

        public override void Init()
        {
            gameObject.SetActive(active);
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}