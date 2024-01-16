using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Scripts.Units;
using UnityEngine;

namespace TowerDefence.Scripts.Sensors
{
	[RequireComponent(typeof(SphereCollider))]
	public class TargetSensor : MonoBehaviour
	{
		public Action<TakeDamageComponent> targetChanged;
		private SphereCollider _collider;
		[SerializeField] private float _sensorRadius;
		protected List<TakeDamageComponent> _targets = new List<TakeDamageComponent>();
		protected TakeDamageComponent _currentTarget;
		private void OnEnable()
		{
			_collider = GetComponent<SphereCollider>();
			_collider.radius = _sensorRadius;
			_collider.isTrigger = true;
			//Init Set radius from config
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out TakeDamageComponent damageTaker))
			{
				_targets.Add(damageTaker);

				damageTaker.onDied += OnAnyTargetDyingOrLeft;

				if(_currentTarget == null)
				{
					GetNextTarget();
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.TryGetComponent(out TakeDamageComponent damageTaker))
			{
				OnAnyTargetDyingOrLeft(damageTaker);
			}
		}

		private void OnAnyTargetDyingOrLeft(TakeDamageComponent takeDamageComponent)
		{
			_targets.Remove(_targets.SingleOrDefault(x => x == takeDamageComponent));

			takeDamageComponent.onDied -= OnAnyTargetDyingOrLeft;
			

			if(takeDamageComponent == _currentTarget)
			{
				GetNextTarget();
			}
		}

		protected virtual void GetNextTarget()
		{
			TakeDamageComponent unit = _targets.FirstOrDefault();
			_currentTarget = unit != null ? unit : null;
			targetChanged?.Invoke(_currentTarget);
		}
	}
}