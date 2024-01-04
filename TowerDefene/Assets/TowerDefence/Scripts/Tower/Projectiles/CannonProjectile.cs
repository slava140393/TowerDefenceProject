using System;
using System.Collections;
using TowerDefence.Scripts.Enemies;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public class CannonProjectile : Projectile
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
		public override void Initialize(Transform target, Transform poolPoint)
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
				transform.position = Vector3.Slerp(_poolPoint.position, _target.position, _posit);

				if(_activateTime + _lifeTime <= Time.time)
					Deactivate();
			}
		}


		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out UnitTreeRunner enemyTreeRunner))
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