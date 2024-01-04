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

		[SerializeField] private TargetSensor _chaseSensor;
		[SerializeField] private TargetSensor _spitSensor;
		[SerializeField] private TargetSensor _meleeSensor;
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
			_chaseSensor.OnTargetEnter += ChaseSensorOnPlayerEnter;
			_chaseSensor.OnTargetExit += ChaseSensorOnPlayerExit;
			_spitSensor.OnTargetEnter += SpitSensorOnPlayerEnter;
			_spitSensor.OnTargetExit += SpitSensorOnPlayerExit;
			_meleeSensor.OnTargetEnter += MeleeSensorOnPlayerEnter;
			_meleeSensor.OnTargetExit += MeleeSensorOnPlayerExit;
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

		private void ChaseSensorOnPlayerEnter(Transform target) => _isInChaseRange = true;
		private void ChaseSensorOnPlayerExit(Vector3 lastKnownPosition) => _isInChaseRange = false;
		private void SpitSensorOnPlayerEnter(Transform target) => _isInSpitRange = true;
		private void SpitSensorOnPlayerExit(Vector3 lastKnownPosition) => _isInSpitRange = false;
		private void MeleeSensorOnPlayerEnter(Transform target) => _isInMeleeRange = true;
		private void MeleeSensorOnPlayerExit(Vector3 lastKnownPosition) => _isInMeleeRange = false;
	}
}