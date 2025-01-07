using UnityEngine;

namespace RTL.Atlas
{
    public class PlayAnimationState : State
    {
        string key;
        Animator animator;
        int layer;

        public PlayAnimationState(string key, Animator animator) : this(key, animator, 0) { }

        public PlayAnimationState(string key, Animator animator, int layer)
        {
            this.key = key;
            this.animator = animator;
            this.layer = layer;
        }

        public override void Init()
        {
            animator.Play(key, layer);
        }

        public override void Update(float timer)
        {
            SelectNextState();
        }
    }
}