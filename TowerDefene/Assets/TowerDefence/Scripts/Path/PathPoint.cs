using System;
using TowerDefence.Scripts.Units.EnemyUnits;
using UnityEngine;

namespace TowerDefence.Scripts.Path
{
	[RequireComponent(typeof(SphereCollider))]
	public class PathPoint : MonoBehaviour
	{
		[SerializeField] private Vector3 _nextPoint = default;
		[SerializeField] private float _pointColliderRadius = 3f;
		private SphereCollider _sphereCollider;
		private void OnValidate()
		{
			if(_sphereCollider == null)
			{
				_sphereCollider = GetComponent<SphereCollider>();
				_sphereCollider.isTrigger = true;
				_sphereCollider.radius = _pointColliderRadius;
			}
		}

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