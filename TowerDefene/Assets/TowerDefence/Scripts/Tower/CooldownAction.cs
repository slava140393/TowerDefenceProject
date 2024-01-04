using System;
using System.Collections;
using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
	public class CooldownAction
	{
		public Action<bool> endCountdown;
		private float _cooldown;
		private MonoBehaviour _mono;
		private float _lastTimeAttack;


		public CooldownAction(float cooldown, MonoBehaviour mono)
		{
			_cooldown = cooldown;
			_mono = mono;

		}

		public void ChangeCooldown(float cooldown)
		{
			_cooldown = cooldown;
		}

		public void StartCountdown()
		{
			_lastTimeAttack = Time.time;

			_mono.StartCoroutine(StartCountdownRoutine());
		}

		private IEnumerator StartCountdownRoutine()
		{
			while(_lastTimeAttack + _cooldown > Time.time)
			{
				yield return null;
			}
			endCountdown?.Invoke(true);
		}
	}
}