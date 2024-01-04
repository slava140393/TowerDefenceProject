using TowerDefence.Scripts.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Scripts.Services
{
	public class Unit : MonoBehaviour
	{
		public NavMeshAgent Agent;
		private UnitTreeRunner _unitTreeRunner;
		public void Initialize(Vector3 lineStartPoint)
		{
			_unitTreeRunner = GetComponent<UnitTreeRunner>();
			_unitTreeRunner.ChangePathPoint(lineStartPoint);
		}

		public void EnableAI() => _unitTreeRunner.EnableAI();
	}
}