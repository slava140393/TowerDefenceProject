using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public class PoolProjectile<T> where T : Projectile
	{
		public T Prefab { get; private set; }
		public Transform Container { get; private set; }

		private List<T> pool;

		public PoolProjectile(T prefab, int count, Transform container)
		{
			this.Prefab = prefab;
			this.Container = container;
			CreatePool(count);
		}

		public bool HasFreeElement(out T element)
		{
			foreach (T projectile in pool)
			{
				if(!projectile.gameObject.activeInHierarchy)
				{
					element = projectile;
					return true;
				}
			}
			element = null;
			return false;
		}

		public T GetFreeElement()
		{
			if(this.HasFreeElement(out T element))
			{
				return element;
			}

			return CreateObject();

		}

		private void CreatePool(int count)
		{
			this.pool = new List<T>();

			for( int i = 0; i < count; i++ )
			{
				this.CreateObject();
			}
		}

		private T CreateObject()
		{
			T createdObject = Object.Instantiate(Prefab, Container);
			createdObject.gameObject.SetActive(false);
			pool.Add(createdObject);
			return createdObject;
		}
	}
}