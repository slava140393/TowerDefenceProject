using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence.Scripts.Services
{
	[CreateAssetMenu(menuName = "TowerDefence/Configs/LevelConfig", fileName = "LevelConfig")]
	public class LevelConfig : ScriptableObject
	{
		[SerializeField] private int _waveCount;
		[SerializeField] private Line[] _lines;


		public Line GetLine() => _lines.First();
	}
}