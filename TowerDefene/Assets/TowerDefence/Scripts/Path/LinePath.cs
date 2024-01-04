using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence.Scripts.Path
{
	public class LinePath : MonoBehaviour
	{
		[SerializeField] private string _name;
		[SerializeField] private Vector3 _spawnUnitPoint;
		[SerializeField] private List<Vector3> _pathPoints;
		public Vector3 SpawnUnitPoint => _spawnUnitPoint;

		public Vector3 GetLineStartPoint() => _pathPoints.First();
		private void OnValidate()
		{
			PathPoint[] paths = GetComponentsInChildren<PathPoint>();
			_pathPoints = paths.Select(x => x.gameObject.transform.position).ToList();

			for( int i = 0; i < paths.Length - 1; i++ )
			{
				paths[i].SetNextPathPoint(_pathPoints[i + 1]);
			}

		}
	}
}