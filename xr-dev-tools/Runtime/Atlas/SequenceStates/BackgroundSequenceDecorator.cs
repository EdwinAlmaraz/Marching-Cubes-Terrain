namespace RTL.Atlas
{
    public class BackgroundSequenceDecorator : BackgroundSequenceState
    {
        State[] states;

        public BackgroundSequenceDecorator(params State[] states) : base()
        {
            this.states = states;

            for (int i = 0; i < states.Length; ++i)
                AddState(states[i]);

            AddState(new EndState());

            FinishStateMachine();        
        }
    }
}