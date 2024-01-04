using System;
using System.Collections;
using TowerDefence.Scripts.Tower.Projectiles;
using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
	public class ActionLauncher : MonoBehaviour
	{
		[SerializeField] private Projectile _projectilePrefab;
		[SerializeField] private Transform _poolPoint;
		[SerializeField] private int _poolCount = 3;
		private PoolProjectile<Projectile> _poolProjectile;
		private CooldownAction _cooldownAction;
		private bool _canAction;

		private TowerActionRadiusSensor _towerActionRadiusSensor;
		private Transform _target;
		private bool _isActive;

		public void Initialize()
		{
			_cooldownAction = new CooldownAction(0.75f, this);
			_cooldownAction.endCountdown += (b => _canAction = b);
			_cooldownAction.StartCountdown();

			_poolProjectile = new PoolProjectile<Projectile>(_projectilePrefab, _poolCount, _poolPoint);

			_towerActionRadiusSensor = gameObject.transform.parent.GetComponentInChildren<TowerActionRadiusSensor>();
			_towerActionRadiusSensor.targetChanged += (t => _target = t);

			_isActive = true;
			StartCoroutine(DoAction());

		}
		public void SetActive(bool isActive) => _isActive = isActive;

		private IEnumerator DoAction()
		{
			while(_isActive)
			{
				yield return new WaitUntil(() => _canAction && _target != null);
				Launch(_target);
			}
			yield return null;
		}

		private void Launch(Transform target)
		{
			_canAction = false;
			Projectile projectile = _poolProjectile.GetFreeElement();
			projectile.Initialize(target, _poolPoint);
			_cooldownAction.StartCountdown();
		}
	}
}