using System;
using TowerDefence.Scripts.Tower.Projectiles;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Scripts.Units.GuardUnits
{
	public class Guard : MonoBehaviour, IPoolableObject
	{
		[SerializeField] private NavMeshAgent _agent;
		private GuardTreeRunner _guardTreeRunner;
		public NavMeshAgent Agent => _agent;
		public GuardTreeRunner TreeRunner => _guardTreeRunner;

		[SerializeField] private Transform _startPoint;
		[SerializeField] private Transform _guardPoint;
		private void Start()
		{
			Initialize(_startPoint,_guardPoint);
		}

		public void Initialize(Transform startPoint, Transform guardPoint)
		{
			_guardTreeRunner = GetComponent<GuardTreeRunner>();
			TreeRunner.Initialize(startPoint,guardPoint);
		}
	}
}