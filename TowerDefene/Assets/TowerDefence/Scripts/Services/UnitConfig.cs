using UnityEngine;

namespace TowerDefence.Scripts.Services
{
	[CreateAssetMenu(menuName = "TowerDefence/Configs/UnitConfig", fileName = "UnitConfig")]
	public class UnitConfig : ScriptableObject
	{
		[SerializeField] private Unit _unitPrefab;


		public Unit UnitPrefab => _unitPrefab;
	}
}