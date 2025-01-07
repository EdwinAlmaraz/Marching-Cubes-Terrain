using UnityEngine;

namespace RTL.Atlas
{
	public abstract class AbstractSceneSequence : SequenceController
	{
		protected virtual void Start()
		{
			InitStateMachine();
			
			AddStates();

			AddState(new EndState());

			FinishStateMachine();
			RunStateMachine();
		}

		protected abstract void AddStates();
	}
}