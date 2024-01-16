using System;
using System.Linq;
using TowerDefence.Scripts.Units;
using UnityEngine;

namespace TowerDefence.Scripts.Sensors
{
	public class AttackSensor : TargetSensor
	{
		public Action<bool> inLineOfSight;
		public Action<bool> inMeleeRange;
		private float _minimumAngle = 10f;
		private float _meleeAttackDistance = 2.5f;

		private void FixedUpdate()
		{
			if(_currentTarget != null)
			{
				Vector3 direction = _currentTarget.transform.position - this.transform.position;
				float angle = Vector3.SignedAngle(this.transform.forward, direction, Vector3.up);

				inLineOfSight?.Invoke(Mathf.Abs(angle) < _minimumAngle);
				inMeleeRange?.Invoke(Vector3.Distance(this.transform.position, _currentTarget.transform.position) < _meleeAttackDistance - 1f);
			}
		}

		protected override void GetNextTarget()
		{
			TakeDamageComponent target = _targets.FirstOrDefault(t => t.Taunted == null);
			_currentTarget = target != null ? target : null;
			targetChanged?.Invoke(_currentTarget);
		}
	}
}