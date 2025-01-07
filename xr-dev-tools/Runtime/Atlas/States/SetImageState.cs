using UnityEngine;
using UnityEngine.UI;

namespace RTL.Atlas
{
    public class SetImageState : State
    {
        private Image image;
        private Sprite sprite;

        public SetImageState(Image image, Sprite sprite)
        {
            this.image = image;
            this.sprite = sprite;
        }

        public SetImageState(GameObject gameObject, Sprite sprite) : this(gameObject.GetComponent<Image>(), sprite) { }
        public SetImageState(Transform transform, Sprite sprite) : this(transform.GetComponent<Image>(), sprite) { }

        public override void Init()
        {
            image.sprite = sprite;
            SelectNextState();
        }

        public override void Update(float timer) { }
    }
}