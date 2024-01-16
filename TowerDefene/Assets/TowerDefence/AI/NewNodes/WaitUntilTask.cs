using RenownedGames.AITree;
using RenownedGames.Apex;
using UnityEngine;

namespace TowerDefence.AI.NewNodes
{
	[NodeContent("WaitUntilTask", "Custom/WaitUntilTask")]
	public class WaitUntilTask : TaskNode
	{
		[Title("WaitBool")]
		[SerializeField]
		private BoolKey boolKey;
		[Title("MaximumWaitTime")]
		[SerializeField]
		private FloatKey waitTimeKey;
		private float _startTime;

		protected override void OnEntry()
		{
			base.OnEntry();
			_startTime = Time.time;


		}

		protected override State OnUpdate()
		{
			if(waitTimeKey == null)
				return State.Failure;

			if(boolKey.GetValue())
				return State.Success;

			if(Time.time - _startTime > waitTimeKey.GetValue())
				return State.Failure;

			return State.Running;
		}

	}
}