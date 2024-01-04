using TowerDefence.Scripts.Enemies;
using UnityEngine;

namespace TowerDefence.Scripts.Path
{
	public class PathPoint : MonoBehaviour
	{
		[SerializeField] private Vector3 _nextPoint = default;

		private void OnTriggerEnter(Collider other)
		{
			if(_nextPoint == default)
			{
				return;
			}

			if(other.TryGetComponent(out UnitTreeRunner enemyTreeRunner))
			{
				enemyTreeRunner.ChangePathPoint(_nextPoint);
			}
		}

		public void SetNextPathPoint(Vector3 nextPoint)
		{
			_nextPoint = nextPoint;
		}
	}
}