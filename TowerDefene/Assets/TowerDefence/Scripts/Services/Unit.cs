using System;
using TowerDefence.Scripts.Tower.Projectiles;
using TowerDefence.Scripts.Units.EnemyUnits;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Scripts.Services
{
	public class Unit : MonoBehaviour,IPoolableObject
	{
		public NavMeshAgent Agent;
		private UnitTreeRunner _unitTreeRunner;

		[SerializeField] private Transform _startPoint;
		private void Start()
		{
			Initialize(_startPoint.position);
		}

		public void Initialize(Vector3 startPoint)
		{
			_unitTreeRunner = GetComponent<UnitTreeRunner>();
			_unitTreeRunner.ChangePathPoint(startPoint);
			EnableAI();
		}

		public void EnableAI() => _unitTreeRunner.EnableAI();

		public void OnTaunted(Transform target)
		{
			Debug.Log($"{target.name } taunt {this.name}");
		}
	}
}