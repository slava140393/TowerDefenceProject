using System;
using RenownedGames.AITree;
using UnityEngine;

namespace DefaultNamespace
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private Target _target;
		[SerializeField] private BehaviourRunner _behaviourRunner;
		private Blackboard _blackboard;

		[SerializeField] private TargetSensor2 _chaseSensor2;
		[SerializeField] private TargetSensor2 _spitSensor2;
		[SerializeField] private TargetSensor2 _meleeSensor2;
		[SerializeField] private Transform _spitSpawnLocation;

		[Header("Attack Configs")]
		[SerializeField] private float _meleeCooldown = 2f;
		[SerializeField] private float _spitCooldown = 5f;
		[SerializeField] private float _bounceCooldown = 10f;

		[Header("Debug")]
		[SerializeField] private bool _isInChaseRange;
		[SerializeField] private bool _isInSpitRange;
		[SerializeField] private bool _isInMeleeRange;
		[SerializeField] private float _lastMeleeTime;
		[SerializeField] private float _lastSpitTime;
		[SerializeField] private float _lastBounceTime;

		private void Start()
		{
			_chaseSensor2.OnTargetEnter += ChaseSensor2OnPlayerEnter;
			_chaseSensor2.OnTargetExit += ChaseSensor2OnPlayerExit;
			_spitSensor2.OnTargetEnter += SpitSensor2OnPlayerEnter;
			_spitSensor2.OnTargetExit += SpitSensor2OnPlayerExit;
			_meleeSensor2.OnTargetEnter += MeleeSensor2OnPlayerEnter;
			_meleeSensor2.OnTargetExit += MeleeSensor2OnPlayerExit;
			_blackboard = _behaviourRunner.GetBlackboard();
		}

		private void Update()
		{
			if(_behaviourRunner == null)
				return;

			SetBlackboardValue(_blackboard, "Target", _target.transform);
			SetBlackboardValue(_blackboard, "IsInChaseRange", _isInChaseRange);
			
			SetBlackboardValue(_blackboard, "IsInMeleeRange", _isInMeleeRange);
			SetBlackboardValue(_blackboard, "CanMeleeAttack", _lastMeleeTime + _meleeCooldown <= Time.time);
			
			SetBlackboardValue(_blackboard, "IsInSpitRange", _isInSpitRange);
			SetBlackboardValue(_blackboard, "SpitSpawnPosition", _spitSpawnLocation.position);
			SetBlackboardValue(_blackboard, "SpitSpawnRotation", _spitSpawnLocation.rotation);
			SetBlackboardValue(_blackboard, "CanSpitAttack", _lastSpitTime + _spitCooldown <= Time.time);
			
			SetBlackboardValue(_blackboard, "CanBounce", _lastBounceTime + _bounceCooldown <= Time.time);
		}

		public void OnMelee()
		{
			_lastMeleeTime = Time.time;
		}

		public void OnSpit()
		{
			_lastSpitTime = Time.time;
		}
		
		public void OnBounce()
		{
			_lastBounceTime = Time.time;
		}

		private void SetBlackboardValue<T>(Blackboard blackboard, string key, T value)
		{
			if(blackboard.TryGetKey(key, out Key keyValue))
			{
				((Key<T>)keyValue).SetValue(value);
			}
		}

		private void ChaseSensor2OnPlayerEnter(Transform target) => _isInChaseRange = true;
		private void ChaseSensor2OnPlayerExit(Vector3 lastKnownPosition) => _isInChaseRange = false;
		private void SpitSensor2OnPlayerEnter(Transform target) => _isInSpitRange = true;
		private void SpitSensor2OnPlayerExit(Vector3 lastKnownPosition) => _isInSpitRange = false;
		private void MeleeSensor2OnPlayerEnter(Transform target) => _isInMeleeRange = true;
		private void MeleeSensor2OnPlayerExit(Vector3 lastKnownPosition) => _isInMeleeRange = false;
	}
}