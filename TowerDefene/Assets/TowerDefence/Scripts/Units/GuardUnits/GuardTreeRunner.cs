using TowerDefence.Scripts.Sensors;
using UnityEngine;

namespace TowerDefence.Scripts.Units.GuardUnits
{

	public class GuardTreeRunner : BaseTreeRunner
	{
		private const string IsInMeleeRange = "IsInMeleeRange";
		private const string InLineOfSight = "InLineOfSight";
		private const string MoveOnStartPoint = "MoveOnStartPoint";
		private const string IsActive = "IsActive";
		private const string Target = "Target";
		private const string HaveTarget = "HaveTarget";
		private const string GuardPoint = "GuardPoint";
		private const string HaveGuardPoint = "HaveGuardPoint";
		private const string AtTheGuardPoint = "AtTheGuardPoint";
		private const string CanAttack = "CanAttack";
		private const string AttackReady = "AttackReady";
		private const string CanTaunt = "CanTaunt";

		[SerializeField] private Transform _startPoint;
		[SerializeField] private Transform _guardPoint;
		private AttackSensor _targetSensor;
		private AttackComponent _attackComponent;


		public void Initialize(Transform startPoint, Transform guardPoint)
		{
			_startPoint = startPoint;
			_guardPoint = guardPoint;
			SetSensor();
			SetAttackComponent();

			OnSpawn();
		}
		private void SetAttackComponent()
		{
			_attackComponent = GetComponent<AttackComponent>();
			_attackComponent.Initialize();
			_attackComponent.attackReady += OnAttackReady;
		}

		private void SetSensor()
		{
			_targetSensor = GetComponentInChildren<AttackSensor>();
			_targetSensor.targetChanged += AddTarget;
			_targetSensor.inLineOfSight += SetIsInLineOfSight;
			_targetSensor.inMeleeRange += SetIsInMeleeRange;
		}


		private void SetIsInMeleeRange(bool isInMeleeRange)
		{
			SetBlackboardValue(Blackboard, IsInMeleeRange, isInMeleeRange);
		}
		private void SetIsInLineOfSight(bool isInLineOfSight)
		{
			SetBlackboardValue(Blackboard, InLineOfSight, isInLineOfSight);
		}
		private void OnSpawn()
		{
			SetBlackboardValue(Blackboard, MoveOnStartPoint, _startPoint.position);
			SetBlackboardValue(Blackboard, IsActive, true);
		}

		private void AddTarget(TakeDamageComponent target)
		{
			if(target.Taunted == null)
			{
				SetBlackboardValue(Blackboard, CanTaunt, true);
			}
			SetBlackboardValue(Blackboard, Target, target.transform);
			SetBlackboardValue(Blackboard, HaveTarget, target != null);
		}

		private void OnAttackReady(string attack)
		{
			SetBlackboardValue(Blackboard, attack, true);
		}

		private void SetGuardPoint()
		{
			SetBlackboardValue(Blackboard, GuardPoint, _guardPoint.position);
			SetBlackboardValue(Blackboard, HaveGuardPoint, true);
			SetBlackboardValue(Blackboard, AtTheGuardPoint, false);
		}


	}
}