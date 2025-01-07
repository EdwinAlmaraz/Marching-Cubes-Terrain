using UnityEngine;

namespace RTL.Atlas
{
    public class DistanceState : State
    {
        private float _distanceRange;
        private bool _isWithin;
        private Transform aT;
        private Transform bT;

        public DistanceState(Transform ObjA, Transform ObjB, float DistanceRange = 0.1f, bool isWithin = true)
        {
            _distanceRange = DistanceRange;
            _isWithin = isWithin;
            aT = ObjA;
            bT = ObjB;
        }

        public DistanceState(GameObject ObjA, GameObject ObjB, float DistanceRange = 0.1f, bool isWithin = true) : this(ObjA.transform, ObjB.transform, DistanceRange, isWithin) { }


        public override void Init() { }

        public override void Update(float timer)
        {

            if (_isWithin)
            {
                if (Vector3.Distance(aT.position, bT.position) < _distanceRange)
                {
                    SelectNextState();
                }
            }
            else
            {
                if (Vector3.Distance(aT.position, bT.position) > _distanceRange)
                {
                    SelectNextState();
                }
            }
        }
    }
}