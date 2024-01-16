using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefence.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace TowerDefence.Scripts.Enemies
{
	public class UnitSpawner : MonoBehaviour
	{
		private Line _line;
		[SerializeField] private float _spawnCooldown = 2f;
		private WaitForSeconds _waitTime;
		[SerializeField] private UnitFactory _unitFactory;

		[SerializeField] private LevelConfig _levelConfig;

		private Coroutine _spawnRoutine;
		private List<UnitConfig> _units;

		private void Start()
		{
			_line = _levelConfig.GetLine();
			_line.SetupPath();
			_waitTime = new WaitForSeconds(_spawnCooldown);
			_units = _line.GetWaveConfig(1).GetUnitConfigs();
			StartSpawn();
		}

		public void StartSpawn()
		{
			StopSpawn();
			_spawnRoutine = StartCoroutine(nameof(SpawnUnits));
		}
		public void StopSpawn()
		{
			if(_spawnRoutine != null)
			{
				StopCoroutine(_spawnRoutine);
			}
		}

		private IEnumerator SpawnUnits()
		{
			int num = 0;

			while(num < _units.Count)
			{
				DoSpawnEnemy(num);
				num++;
				yield return _waitTime;
			}
			yield return null;
		}
		private void DoSpawnEnemy(int num)
		{
			Unit unit = _unitFactory.GetUnit(_units[num], _line.GetLineStartPoint());
			unit.Agent.Warp(_line.GetLineSpawnPoint());
			unit.EnableAI();
		}
	}
}