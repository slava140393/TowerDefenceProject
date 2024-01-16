using System.Collections;
using TowerDefence.Scripts.Units;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public class CurveProjectile : Projectile
	{
		[SerializeField] private AnimationCurve _curve;
		[SerializeField] private float _duration = 1f;
		[SerializeField] private float _heightY = 3f;
		private float _activateTime;
		public override void Initialize(TakeDamageComponent target, Transform poolPoint)
		{
			_target = target;
			_poolPoint = poolPoint;
			Activate();
		}

		private IEnumerator Move()
		{
			_activateTime = 0;
			Vector3 end = _target.transform.position;

			while(_activateTime <= _duration)
			{
				_activateTime += Time.deltaTime;
				float linearT = _activateTime / _duration;
				float heightT = _curve.Evaluate(linearT);
				float height = Mathf.Lerp(0, _heightY, heightT);
				transform.position = Vector3.Lerp(_poolPoint.position, end, linearT) + new Vector3(0, height, 0);
				yield return null;
			}
			Deactivate();
		}



		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out TakeDamageComponent damageTaker))
			{
				Deactivate();
			}
		}
		private void Activate()
		{
			this.gameObject.SetActive(true);
			_projectileMoveRoutine = StartCoroutine(Move());

		}
		private void Deactivate()
		{
			this.gameObject.SetActive(false);
			StopCoroutine(_projectileMoveRoutine);
		}
	}
}