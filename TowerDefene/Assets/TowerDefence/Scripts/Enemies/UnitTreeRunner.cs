using RenownedGames.AITree;
using UnityEngine;

namespace TowerDefence.Scripts.Enemies
{
	public class UnitTreeRunner : MonoBehaviour
	{
		private const string NextPathPoint = "NextPathPoint";
		private const string EnableAIToggle = "EnableAIToggle";
		private BehaviourRunner _behaviourRunner;
		private Blackboard _blackboard;
		private void Awake()
		{
			_behaviourRunner = GetComponent<BehaviourRunner>();
			_blackboard = _behaviourRunner.GetBlackboard();
			SetBlackboardValue(_blackboard, EnableAIToggle, false);
		}

		public void ChangePathPoint(Vector3 pathPoint)
		{
			SetBlackboardValue(_blackboard, NextPathPoint, pathPoint);
		}

		private void SetBlackboardValue<T>(Blackboard blackboard, string key, T value)
		{
			if(blackboard.TryGetKey(key, out Key keyValue))
			{
				((Key<T>)keyValue).SetValue(value);
			}
		}

		public void EnableAI() => SetBlackboardValue(_blackboard, EnableAIToggle, true);
	}
}