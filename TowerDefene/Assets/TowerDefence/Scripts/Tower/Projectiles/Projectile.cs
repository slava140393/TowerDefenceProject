using TowerDefence.Scripts.Units;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public abstract class Projectile : MonoBehaviour,IPoolableObject
	{
		protected TakeDamageComponent _target;
		protected Transform _poolPoint;
		protected Coroutine _projectileMoveRoutine;

		public abstract void Initialize(TakeDamageComponent target, Transform poolPoint);
	}
}