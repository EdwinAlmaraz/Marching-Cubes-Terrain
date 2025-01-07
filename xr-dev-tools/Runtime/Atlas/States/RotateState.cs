using UnityEngine;

namespace RTL.Atlas
{
    public class RotateState : State
    {
        GameObject obj;
        Transform pivot;
        Vector3 axis;
        float angle;
        float time;

        public RotateState(GameObject obj, Transform pivot, Vector3 axis, float angle, float time = 1f)
        {
            this.obj = obj;
            this.pivot = pivot;
            this.angle = angle;
            this.axis = axis;
            this.time = time;
        }

        public override void Init()
        {
            if (time == 0)
            {
                obj.transform.RotateAround(pivot.position, axis, angle);
                SelectNextState();
            }
        }

        public override void Update(float timer)
        {
            float progress = timer / time;

            obj.transform.RotateAround(pivot.position, axis, angle * Time.deltaTime / time);

            if (progress >= 1)
            {
                SelectNextState();
            }
        }
    }
}