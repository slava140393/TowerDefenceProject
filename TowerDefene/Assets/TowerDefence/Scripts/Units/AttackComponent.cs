using System;
using TowerDefence.Scripts.Tower;
using UnityEngine;

namespace TowerDefence.Scripts.Units
{
	public class AttackComponent : MonoBehaviour
	{
		public Action<string> attackReady;
		[SerializeField] private float _cooldownTime = 1.5f;
		private const string AttackReady = "AttackReady";
		private CooldownAction _cooldownAction;
		private TakeDamageComponent _target;

		public void Initialize()
		{
			_cooldownAction = new CooldownAction(_cooldownTime, this);
			_cooldownAction.endCountdown += x => attackReady?.Invoke(AttackReady);
			_cooldownAction.StartCountdown();
		}

		private void OnAttack()
		{
			_cooldownAction.StartCountdown();

			if(_target!=null)
			{
				Debug.Log("HaveTarget");
				_target = null;
			}
		}

		private void AttackTarget(Transform target) => _target = target.GetComponent<TakeDamageComponent>();
	}
	public enum TypeAttack
	{
		Melee,
		Range,
	}
}