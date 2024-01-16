using System.Collections;
using TowerDefence.Scripts.Sensors;
using TowerDefence.Scripts.Tower.Projectiles;
using TowerDefence.Scripts.Units;
using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
	public class ProjectileLauncher : MonoBehaviour
	{
		[SerializeField] private Projectile _projectilePrefab;
		[SerializeField] private Transform _poolPoint;
		[SerializeField] private int _poolCount = 3;
		[SerializeField] private float _cooldown = 0.75f;
		private PoolObject<Projectile> _poolObject;
		private CooldownAction _cooldownAction;
		private bool _canAction;

		private TargetSensor _targetSensor;
		private TakeDamageComponent _target;
		private bool _isActive;

		public void Initialize()
		{
			_cooldownAction = new CooldownAction(_cooldown, this);
			_cooldownAction.endCountdown += (b => _canAction = b);
			_cooldownAction.StartCountdown();

			_poolObject = new PoolObject<Projectile>(_projectilePrefab, _poolCount, _poolPoint);

			_targetSensor = gameObject.transform.parent.GetComponentInChildren<TargetSensor>();
			_targetSensor.targetChanged += (t => _target = t);

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

		private void Launch(TakeDamageComponent target)
		{
			_canAction = false;
			Projectile projectile = _poolObject.GetFreeElement();
			projectile.Initialize(target, _poolPoint);
			_cooldownAction.StartCountdown();
		}
	}
}