using UnityEngine;

namespace RTL.Atlas
{
    public class TeleportationSequence : SequenceState
    {
        private Transform toTeleport;
        private Transform destination;
        private Vector3 positionOffset;
        private Vector3 rotationOffset;

        public TeleportationSequence(Transform toTeleport, Transform destination, Vector3 positionOffset = default(Vector3), Vector3 rotationOffset = default(Vector3)) : base()
        {
            this.toTeleport = toTeleport;
            this.destination = destination;
            this.positionOffset = positionOffset;
            this.rotationOffset = rotationOffset;

            AddStates();
        }

        public TeleportationSequence(GameObject toTeleport, Transform destination, Vector3 positionOffset = default(Vector3), Vector3 rotationOffset = default(Vector3)) : this(toTeleport.transform, destination, positionOffset, rotationOffset) { }

        public TeleportationSequence(GameObject toTeleport, GameObject destination, Vector3 positionOffset = default(Vector3), Vector3 rotationOffset = default(Vector3)) : this(toTeleport.transform, destination.transform, positionOffset, rotationOffset) { }

        protected void AddStates()
        {
            AddState(new SetPositionState(toTeleport, destination, positionOffset));
            AddState(new SetRotationState(toTeleport, destination, rotationOffset));

            AddState(new EndState());
            FinishStateMachine();
        }
    }
}