using RenownedGames.AITree;
using RenownedGames.Apex;
using UnityEngine;

namespace TowerDefence.AI.NewNodes
{
	[NodeContent("LookAtTask", "Custom/LookAt")]
	public class LookAtTask : TaskNode
	{
		[Title("SelfTransform")]
		[SerializeField]
		private TransformKey _selfTransformKey;
		[Title("TargetTransform")]
		[SerializeField]
		private TransformKey _targetTransformKey;
		[Title("RotateSpeed")]
		[SerializeField]
		private FloatKey _speedKey;
		private Transform _targetTransform;
		private Transform _selfTransform;
		private float _speed;

		protected override void OnEntry()
		{
			base.OnEntry();
			_targetTransform = _targetTransformKey.GetValue();
			_selfTransform = _selfTransformKey.GetValue();
			_speed = _speedKey.GetValue();
		}

		protected override State OnUpdate()
		{
			Vector3 direction = _targetTransform.position - _selfTransform.position;
			Vector3 newDirection = Vector3.RotateTowards(_selfTransform.forward, direction, _speed * Time.deltaTime, 0f);
			_selfTransform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
			float angle = Vector3.SignedAngle(_selfTransform.forward, direction, Vector3.up);

			if(Mathf.Abs(angle) < 10f)
			{
				return State.Success;
			}
			return State.Running;
		}

	}
}