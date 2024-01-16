using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public class PoolObject<T> where T : MonoBehaviour,IPoolableObject
	{
		public T Prefab { get; private set; }
		public Transform Container { get; private set; }

		private List<T> pool;

		public PoolObject(T prefab, int count, Transform container)
		{
			this.Prefab = prefab;
			this.Container = container;
			CreatePool(count);
		}

		public bool HasFreeElement(out T element)
		{
			foreach (T obj in pool)
			{
				if(!obj.gameObject.activeInHierarchy)
				{
					element = obj;
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

		public int CountActiveObjects() => pool.Count(o => o.gameObject.activeInHierarchy);

	}

}