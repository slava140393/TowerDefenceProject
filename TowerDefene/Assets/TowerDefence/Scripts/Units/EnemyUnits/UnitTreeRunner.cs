using UnityEngine;

namespace TowerDefence.Scripts.Units.EnemyUnits
{
	public class UnitTreeRunner : BaseTreeRunner
	{
		private const string NextPathPoint = "NextPathPoint";
		private const string EnableAIToggle = "EnableAIToggle";
		private const string Target = "Target";
		private const string HaveTarget = "HaveTarget";

		protected override void Awake()
		{
			base.Awake();
			SetBlackboardValue(this.Blackboard, EnableAIToggle, false);
		}

		public void ChangePathPoint(Vector3 pathPoint)
		{
			SetBlackboardValue(this.Blackboard, NextPathPoint, pathPoint);
		}
		public void EnableAI() => SetBlackboardValue(this.Blackboard, EnableAIToggle, true);

		public void SetTarget(Transform target)
		{
			Debug.Log("SetTarget");
			SetBlackboardValue(this.Blackboard,Target,target);
			SetBlackboardValue(this.Blackboard,HaveTarget,true);
		}

	}
}