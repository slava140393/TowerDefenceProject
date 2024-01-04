using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Scripts.Services
{
	[CreateAssetMenu(menuName = "TowerDefence/Configs/WaveConfig", fileName = "WaveConfig")]
	[Serializable]
	public class WaveConfig : ScriptableObject
	{
		[SerializeField] private List<UnitConfig> _unitConfigs;

		public List<UnitConfig> GetUnitConfigs() => _unitConfigs;
	}
}