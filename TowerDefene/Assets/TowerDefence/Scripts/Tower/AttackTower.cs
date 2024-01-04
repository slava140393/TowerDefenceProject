using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
	public class AttackTower : Tower
	{
		[SerializeField] private ActionLauncher _actionLauncher;

		private void Start()
		{
			_actionLauncher.Initialize();
		}
		
	}
}