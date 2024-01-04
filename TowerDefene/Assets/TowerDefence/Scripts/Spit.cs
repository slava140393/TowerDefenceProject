using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spit : MonoBehaviour
{
	[SerializeField] private float _autoDestroyTime = 1f;
	[SerializeField] private float _force = 100f;

	private WaitForSeconds _waitForSeconds;
	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		StopAllCoroutines();
		StartCoroutine(DelayDisable());
	}
	private IEnumerator DelayDisable()
	{
		if(_waitForSeconds == null)
		{
			_waitForSeconds = new WaitForSeconds(_autoDestroyTime);
		}
		yield return null;
		_rigidbody.AddForce(transform.forward * _force);
		yield return _waitForSeconds;
		gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		_rigidbody.angularVelocity = Vector3.zero;
		_rigidbody.velocity = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out Target target))
		{
			target.OnDamage(15);
			gameObject.SetActive(false);
		}
	}
}