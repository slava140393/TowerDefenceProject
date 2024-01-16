using RenownedGames.AITree;
using UnityEngine;

namespace TowerDefence.Scripts.Units
{
	public abstract class BaseTreeRunner : MonoBehaviour
	{
		private BehaviourRunner _behaviourRunner;
		private Blackboard _blackboard;
		public BehaviourRunner Runner => _behaviourRunner;
		public Blackboard Blackboard => _blackboard;
		protected virtual void Awake()
		{
			_behaviourRunner = GetComponent<BehaviourRunner>();
			_blackboard = Runner.GetBlackboard();

		}
		protected void SetBlackboardValue<T>(Blackboard blackboard, string key, T value)
		{
			if(blackboard.TryGetKey(key, out Key keyValue))
			{
				((Key<T>)keyValue).SetValue(value);
			}
		}

	}
}