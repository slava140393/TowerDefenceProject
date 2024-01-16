using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
	public class AttackTower : Tower
	{
		[SerializeField] private ProjectileLauncher _projectileLauncher;

		private void Start()
		{
			_projectileLauncher.Initialize();
		}
		
	}
}