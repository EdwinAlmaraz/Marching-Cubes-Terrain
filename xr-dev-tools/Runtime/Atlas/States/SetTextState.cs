using UnityEngine;
using TMPro;

namespace RTL.Atlas
{
    public class SetTextState : State
    {
        private TMP_Text text;
        private string message;

        public SetTextState(TMP_Text text, string message)
        {
            if (text == null)
                Debug.LogError("Null TextMeshPro");

            this.text = text;
            this.message = message;
        }

        public SetTextState(GameObject gameObject, string message) : this(gameObject.GetComponent<TMP_Text>(), message) { }

        public override void Init()
        {
            text.text = message;
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}