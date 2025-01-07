using UnityEngine;

namespace RTL.Atlas
{
    public class EnableComponentState<T> : State where T : Component
    {
        private bool active;
        private Component component;

        public EnableComponentState(Component component, bool active = true)
        {
            this.component = component;
            this.active = active;

            if (component == null)
                Debug.LogError("Invalid Component");
        }

        public EnableComponentState(GameObject gameObject, bool active = true)
        {
            this.active = active;
            component = gameObject.GetComponent<T>();

            if (component == null)
                Debug.LogError("Invalid Component: " + gameObject.name);
        }

        public EnableComponentState(Transform transform, bool active = true) : this(transform.gameObject, active) { }

        public EnableComponentState(GameObject gameObject, int index, bool active = true)
        {
            this.active = active;

            component = null;
            T[] componentList = gameObject.GetComponents<T>();

            if (index < componentList.Length)
                component = componentList[index];
            else
                Debug.LogError("Invalid Component: " + gameObject.name);
        }

        public EnableComponentState(Transform transform, int index, bool active = true) : this(transform.gameObject, index, active) { }

        public override void Init()
        {
            // CODE DIAPER ALERT!!!
            // Use Adapter Pattern Instead

            if (component != null)
            {
                Behaviour behavior = component as Behaviour;
                if (behavior != null)
                    behavior.enabled = active;
                else
                {
                    Collider collider = component as Collider;
                    if (collider != null)
                        collider.enabled = active;
                    else
                    {
                        Renderer renderer = component as Renderer;
                        if (renderer != null)
                            renderer.enabled = active;
                        else
                        {
                            Debug.Log("No Avaliable Downcast for Component!");
                        }
                    }
                }
            }

            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}