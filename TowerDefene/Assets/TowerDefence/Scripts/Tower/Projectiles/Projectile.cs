using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public abstract class Projectile : MonoBehaviour
	{
		protected Transform _target;
		protected Transform _poolPoint;
		protected Coroutine _projectileMoveRoutine;

		public abstract void Initialize(Transform target, Transform poolPoint);
	}
}