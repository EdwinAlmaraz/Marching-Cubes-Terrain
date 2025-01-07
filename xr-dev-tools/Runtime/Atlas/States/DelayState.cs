namespace RTL.Atlas
{
    public class DelayState : State
    {
		float delay;

		public DelayState(float delay) { this.delay = delay; }

		public override void Init() { }

		public override void Update(float timer)
		{
			if(timer > delay) SelectNextState(); 
		}
    }
}