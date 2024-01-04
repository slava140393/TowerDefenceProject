using System.Collections;
using TowerDefence.Scripts.Enemies;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public class ParabolaProjectile : Projectile
	{
		[SerializeField] private float _initialVelocity;
		[SerializeField] private float _angle;

		private float _activateTime;

		public override void Initialize(Transform target, Transform poolPoint)
		{
			_poolPoint = poolPoint;
			transform.position = poolPoint.position;
			Activate();

			//	float angle = _angle * Mathf.Deg2Rad;

			Vector3 direction = target.position - poolPoint.position;
			Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
			Vector3 targetPosition = new Vector3(groundDirection.magnitude, direction.y, 0);

			float height = targetPosition.y + targetPosition.magnitude / 2;
			height = Mathf.Max(0.01f, height);
			float angle;
			float v0;
			float time;
			CalculatePathWithHeight(targetPosition, height, out v0, out angle, out time);

			_projectileMoveRoutine = StartCoroutine(ProjectileMovement(groundDirection.normalized, v0, angle, time));
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out UnitTreeRunner enemyTreeRunner))
			{
				Deactivate();
			}
		}

		private void Activate()
		{
			this.gameObject.SetActive(true);
			_activateTime = Time.time;
		}
		private void Deactivate()
		{
			this.gameObject.SetActive(false);
			StopCoroutine(_projectileMoveRoutine);
		}

		private void CalculatePathWithHeight(Vector3 targetPosition, float h, out float v0, out float angle, out float time)
		{
			float xt = targetPosition.x;
			float yt = targetPosition.y;
			float g = -Physics.gravity.y;

			float b = Mathf.Sqrt(2 * g * h);
			float a = (-0.5f * g);
			float c = -yt;

			float tplus = QuadraticEquation(a, b, c, 1);
			float tmin = QuadraticEquation(a, b, c, -1);
			time = tplus > tmin ? tplus : tmin;
			angle = Mathf.Atan(b * time / xt);
			v0 = b / Mathf.Sin(angle);
		}
		private float QuadraticEquation(float a, float b, float c, int sign)
		{
			return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
		}

		private void CalculatePath(Vector3 targetPosition, float angle, out float v0, out float time)
		{
			float xt = targetPosition.x;
			float yt = targetPosition.y;
			float g = -Physics.gravity.y;

			float v1 = Mathf.Pow(xt, 2) * g;
			float v2 = 2 * xt * Mathf.Sin(angle) * Mathf.Cos(angle);
			float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);
			v0 = Mathf.Sqrt(v1 / (v2 / v3));

			time = xt / (v0 * Mathf.Cos(angle));
		}

		private IEnumerator ProjectileMovement(Vector3 direction, float v0, float angle, float time)
		{
			float t = 0;

			while(t < time)
			{
				float x = v0 * t * Mathf.Cos(angle);
				float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
				transform.position = _poolPoint.position + direction * x + Vector3.up * y;
				t += Time.deltaTime;
				yield return null;
			}
			Deactivate();
		}
	}
}