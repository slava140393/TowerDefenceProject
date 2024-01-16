using System;
using TowerDefence.Scripts.Units;
using TowerDefence.Scripts.Units.GuardUnits;
using UnityEngine;

namespace TowerDefence.Scripts.Sensors
{
	public class EnemyAttackSensor : AttackSensor
	{
		private Guard _guard;
		private void Start()
		{
			targetChanged += OnTargetChanged;
		}
		private void OnTargetChanged(TakeDamageComponent target)
		{
			_guard = target.GetComponent<Guard>();
			//GetNextTarget();
		}
	}
}