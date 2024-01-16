using System.Collections;
using TowerDefence.Scripts.Sensors;
using TowerDefence.Scripts.Tower.Projectiles;
using TowerDefence.Scripts.Units.GuardUnits;
using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
	public class GuardLauncher : MonoBehaviour
	{
		[SerializeField] private Guard _guardPrefab;
		[SerializeField] private Transform _poolPoint;
		[SerializeField] private int _poolCount = 3;
		[SerializeField] private Transform _startPoint;
		[SerializeField] private Transform _guardPoint;
		private PoolObject<Guard> _poolObject;
		private CooldownAction _cooldownAction;
		private bool _canAction;

		private bool _isActive;

		public void Initialize()
		{
			_cooldownAction = new CooldownAction(1.5f, this);
			_cooldownAction.endCountdown += (b => _canAction = b);
			_cooldownAction.StartCountdown();

			_poolObject = new PoolObject<Guard>(_guardPrefab, _poolCount, _poolPoint);

			_isActive = true;
			StartCoroutine(DoAction());

		}
		public void SetActive(bool isActive) => _isActive = isActive;

		private IEnumerator DoAction()
		{
			while(_isActive)
			{
				yield return new WaitUntil(() => _canAction && _poolObject.CountActiveObjects() < _poolCount);
				Launch();
			}
			yield return null;
		}

		private void Launch()
		{
			_canAction = false;
			Guard guard = _poolObject.GetFreeElement();
			guard.Initialize(_startPoint, _guardPoint);
			guard.Agent.Warp(_poolPoint.position);
			guard.gameObject.SetActive(true);
			_cooldownAction.StartCountdown();
		}
	}
}