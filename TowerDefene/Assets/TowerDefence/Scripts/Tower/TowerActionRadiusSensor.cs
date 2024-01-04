using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Scripts.Enemies;
using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
	public class TowerActionRadiusSensor : MonoBehaviour
	{
		public Action<Transform> targetChanged;
		private Collider _collider;
		private float _attackRadius;
		private List<UnitTreeRunner> _targets = new List<UnitTreeRunner>();
		private Transform _currentTarget;
		private void OnEnable()
		{
			_collider = GetComponent<Collider>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out UnitTreeRunner enemyTreeRunner))
			{
				_targets.Add(enemyTreeRunner);

				//subscribe on dying
				if(_currentTarget == null)
				{
					GetNextTarget();
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.TryGetComponent(out UnitTreeRunner enemyTreeRunner))
			{
				_targets.Remove(enemyTreeRunner);

				//unsubscribe on dying
				if(enemyTreeRunner.transform == _currentTarget)
				{
					GetNextTarget();
				}
			}
		}

		private void OnAnyTargetDying(Transform dyingTarget)
		{
			if(_currentTarget == dyingTarget)
			{
				GetNextTarget();
			}
		}

		private void GetNextTarget()
		{
			UnitTreeRunner unit = _targets.FirstOrDefault();
			_currentTarget = unit != null ? unit.transform : null;
			targetChanged?.Invoke(_currentTarget);
		}
	}
}