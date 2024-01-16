using RenownedGames.AITree;
using TowerDefence.Scripts.Sensors;
using TowerDefence.Scripts.Units;
using UnityEngine;

public class testtarget : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private Transform _target2;
	[SerializeField] private Transform _startPoint;
	[SerializeField] private Transform _guardPoint;
	[SerializeField] private bool _haveGuardPoint;

	private BehaviourRunner _behaviourRunner;
	private Blackboard _blackboard;
	private AttackSensor _targetSensor;
	void Start()
	{
		_behaviourRunner = GetComponent<BehaviourRunner>();
		_blackboard = _behaviourRunner.GetBlackboard();

		_targetSensor = GetComponentInChildren<AttackSensor>();
		_targetSensor.targetChanged += AddTarget;
		_targetSensor.inLineOfSight += SetIsInLineOfSight;
		_targetSensor.inMeleeRange += SetIsInMeleeRange;

		OnSpawn();
	}
	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Q))
		{
			ToGuardPoint();
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			Attack();
		}
		


	}
	private void SetIsInMeleeRange(bool isInMeleeRange)
	{
		SetBlackboardValue(_blackboard, "IsInMeleeRange", isInMeleeRange);
	}
	private void SetIsInLineOfSight(bool isInLineOfSight)
	{
		SetBlackboardValue(_blackboard, "InLineOfSight", isInLineOfSight);
	}
	private void OnSpawn()
	{
		SetBlackboardValue(_blackboard, "MoveOnStartPoint", _startPoint.position);
		SetBlackboardValue(_blackboard, "IsActive", true);
	}

	private void Attack()
	{
		SetBlackboardValue(_blackboard, "CanAttack", true);
		SetBlackboardValue(_blackboard, "AttackReady", true);
	}

	private void ToGuardPoint()
	{
		SetBlackboardValue(_blackboard, "GuardPoint", _guardPoint.position);
		SetBlackboardValue(_blackboard, "HaveGuardPoint", true);
		SetBlackboardValue(_blackboard, "AtTheGuardPoint", false);
	}

	private void AddTarget(TakeDamageComponent target)
	{
		SetBlackboardValue(_blackboard, "Target", target.transform);
		SetBlackboardValue(_blackboard, "HaveTarget", target != null);
	}

	protected void SetBlackboardValue<T>(Blackboard blackboard, string key, T value)
	{
		// Debug.Log(typeof(T));

		if(blackboard.TryGetKey(key, out Key keyValue))
		{
			((Key<T>)keyValue).SetValue(value);
		}
	}
}