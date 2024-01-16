using TowerDefence.Scripts.Units;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public class ArcherProjectile : Projectile
	{
		[SerializeField] private float _speed = 2f;
		private float _posit;
		private bool _canMove;
		private float _lifeTime = 2f;
		private float _activateTime;
		private void Awake()
		{
			Deactivate();
		}
		public override void Initialize(TakeDamageComponent target, Transform poolPoint)
		{
			_target = target;
			_poolPoint = poolPoint;
			_posit = 0;
			transform.position = poolPoint.position;
			Activate();
		}

		private void Update()
		{
			if(_canMove)
			{
				_posit += Time.deltaTime * _speed;
				Vector3 targetPosition = _target.transform.position;
				transform.position = Vector3.Slerp(_poolPoint.position, targetPosition, _posit);
				transform.LookAt(targetPosition);

				if(_activateTime + _lifeTime <= Time.time)
					Deactivate();
			}
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
			_canMove = true;
			_activateTime = Time.time;

		}
		private void Deactivate()
		{
			this.gameObject.SetActive(false);
			_canMove = false;
		}
	}
}