using System;
using TowerDefence.Scripts.Units.EnemyUnits;
using UnityEngine;

namespace TowerDefence.Scripts.Units
{
	public class TakeDamageComponent : MonoBehaviour
	{
		public Action<TakeDamageComponent> onDied;
		[SerializeField] private bool _isDead;

		public TakeDamageComponent Taunted { get; set; }

		private void Update()
		{
			if(_isDead == true)
			{
				onDied?.Invoke(this);
				_isDead = false;
			}
		}

		public void OnTaunt(Transform target)
		{
			Debug.Log($"OnTaunt {target.name}");
			TakeDamageComponent takeDamageComponent = target.GetComponent<TakeDamageComponent>();
			takeDamageComponent.onDied += d => Taunted = null;
			Taunted = takeDamageComponent;
			GetComponent<UnitTreeRunner>().SetTarget(target);
		}

		public void TakeDamage(int damage)
		{

		}
	}
}