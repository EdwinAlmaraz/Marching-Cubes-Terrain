using UnityEngine;

namespace RTL.Atlas
{
	public class PlayAudioAtPositionState : State
	{
		private Transform _transform;
		private AudioClip _audioClip;

		public PlayAudioAtPositionState(AudioClip audioClip, Transform transform)
		{
			_audioClip = audioClip;
			_transform = transform;
		}

		public override void Init()
		{
			AudioSource.PlayClipAtPoint(_audioClip, _transform.position);
			SelectNextState();
		}

		public override void Update(float timer) { }
	}
}