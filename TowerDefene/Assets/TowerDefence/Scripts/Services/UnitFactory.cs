using UnityEngine;

namespace TowerDefence.Scripts.Services
{
	public class UnitFactory : MonoBehaviour 
	{
		public Unit GetUnit(UnitConfig unitConfig, Vector3 lineStartPoint)
		{
			Unit unit = Instantiate(unitConfig.UnitPrefab);
			unit.Initialize(lineStartPoint);
			return unit;
		}
	}
}