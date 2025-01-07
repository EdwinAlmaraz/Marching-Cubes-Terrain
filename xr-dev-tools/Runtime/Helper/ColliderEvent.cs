using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class ColliderEvent : MonoBehaviour
{
	[SerializeField] private UnityEvent<Collider> _onTriggerEnter;
	public UnityEvent<Collider> onTriggerEnter => _onTriggerEnter;

	[SerializeField] private UnityEvent<Collider> _onTriggerStay;
	public UnityEvent<Collider> onTriggerStay => _onTriggerStay;

	[SerializeField] private UnityEvent<Collider> _onTriggerExit;
	public UnityEvent<Collider> onTriggerExit => _onTriggerExit;

	[SerializeField] private UnityEvent<Collision> _onCollisionEnter;
	public UnityEvent<Collision> onCollisionEnter => _onCollisionEnter;

	[SerializeField] private UnityEvent<Collision> _onCollisionStay;
	public UnityEvent<Collision> onCollisionStay => _onCollisionStay;

	[SerializeField] private UnityEvent<Collision> _onCollisionExit;
	public UnityEvent<Collision> onCollisionExit => _onCollisionExit;

	private void OnTriggerEnter(Collider other)
	{
		_onTriggerEnter.Invoke(other);
	}

	private void OnTriggerStay(Collider other)
	{
		_onTriggerStay.Invoke(other);
	}

	private void OnTriggerExit(Collider other)
	{
		_onTriggerExit.Invoke(other);
	}

	private void OnCollisionEnter(Collision collision)
	{
		_onCollisionEnter.Invoke(collision);
	}

	private void OnCollisionStay(Collision collision)
	{
		_onCollisionStay.Invoke(collision);
	}

	private void OnCollisionExit(Collision collision)
	{
		_onCollisionExit.Invoke(collision);
	}
}
