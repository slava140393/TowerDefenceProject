using System;
using System.Collections.Generic;
using TowerDefence.Scripts.Path;
using UnityEngine;

namespace TowerDefence.Scripts.Services
{
	[Serializable]
	public class Line
	{
		[SerializeField] private LinePath _linePath;
		[SerializeField] private List<WaveConfig> _waveConfigs;

		public void SetupPath() => GameObject.Instantiate(_linePath);
		public Vector3 GetLineSpawnPoint() => _linePath.SpawnUnitPoint;
		public Vector3 GetLineStartPoint() => _linePath.GetLineStartPoint();
		public WaveConfig GetWaveConfig(int waveNumber)
		{
			if(_waveConfigs.Count >= waveNumber)
			{
				return _waveConfigs[waveNumber - 1];

			}
			else
			{
				throw new ArgumentException($"{typeof(Line)} has less waveConfigs than waves in level");
			}
		}
	}
}