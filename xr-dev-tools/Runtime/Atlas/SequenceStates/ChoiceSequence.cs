// Author(s):       Edwin Almaraz, Max Calhoun
// Updated:         2/3/2023
// Description:     SequenceState used in scene sequence controller to spawn a choice UI with up to 4 options.  Should be followed by TRIGGERSWITCH("option1", "option2"...
//                  Use Choice.instance.TurnOff() after a choice is selected.

namespace RTL.Atlas
{
    public class ChoiceSequence : SequenceState
    {
        Choice choice;
        string prompt;
        string[] choicesText;
        SequenceController sequence;
        string[] triggerIDs;

        public ChoiceSequence(SequenceController sequence, string prompt, string[] choicesText) :
            base()
        {
            this.sequence = sequence;
            choice = Choice.instance;
            this.prompt = prompt;
            this.choicesText = choicesText;
            this.triggerIDs = new string[choicesText.Length];
            AddStates();
        }

        public override void Init()
        {
            base.Init();
            choice.Deregister();
            //register triggers
            triggerIDs = choice.Register(sequence, choicesText.Length);
        }

        public override void Trigger(string id)
        {
            base.Trigger(id);
            choice.Deregister();
        }

        protected void AddStates()
        {
            AddState(new ActionState(() => choice.Deregister()));
            AddState(new SetActiveState(choice, false));
            AddState(new ActionState(() => choice.SetChoiceText(choicesText)));
            AddState(new SetTextState(choice.GetComponent<Choice>().Prompt, prompt));
            AddState(new SetActiveState(choice));
            AddState(new EndState());
            FinishStateMachine();
        }
    }
}