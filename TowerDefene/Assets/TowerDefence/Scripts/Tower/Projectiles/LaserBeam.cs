using System;
using System.Collections;
using TowerDefence.Scripts.Units;
using UnityEngine;

namespace TowerDefence.Scripts.Tower.Projectiles
{
	public class LaserBeam : Projectile
	{
		[SerializeField] private float _hitOffset = 0.01f;
		[SerializeField] private float _speed = 5f;
		[SerializeField] private float _duration = 1.5f;
		[SerializeField] private float _maximumLenght = 20f;
		[SerializeField] private GameObject _endBeamGameObject;
		[SerializeField] private LineRenderer _laser;
		[SerializeField] private ParticleSystem[] _effects;
		[SerializeField] private LayerMask _layer;
		private float _activateTime;

		public override void Initialize(TakeDamageComponent target, Transform poolPoint)
		{
			_target = target;
			_poolPoint = poolPoint;
			Activate();
		}


		private IEnumerator Move()
		{
			_activateTime = 0;
			Vector3 end = _target.transform.position;
			Vector3 start = _poolPoint.transform.position;
			Vector3 groundStart = new Vector3(start.x, 0, start.z);


			while(_activateTime <= _duration)
			{
				if(_laser != null)
				{
					RaycastHit hit;
					Vector3 point = Vector3.LerpUnclamped(groundStart, end, 0.15f + _activateTime * _speed);

					Vector3 direction = point - start;

					if(Physics.Raycast(start, direction, out hit, _maximumLenght, _layer))
					{
						this.transform.LookAt(point);
						float distance = Vector3.Distance(start, hit.point);
						_laser.SetPosition(1, new Vector3(0, 0, distance));
						_endBeamGameObject.transform.position = hit.point + hit.normal * _hitOffset;
						_endBeamGameObject.transform.LookAt(hit.point + hit.normal);

						if(_maximumLenght - 1 < distance)
						{
							Deactivate();
						}
					}


				}
				_activateTime += Time.deltaTime;
				yield return null;
			}
			Deactivate();
		}


		private void Activate()
		{
			this.gameObject.SetActive(true);
			_laser.gameObject.SetActive(true);
			_projectileMoveRoutine = StartCoroutine(Move());

		}

		private void Deactivate()
		{
			_laser.gameObject.SetActive(false);
			this.gameObject.SetActive(false);
			StopCoroutine(_projectileMoveRoutine);
		}
	}
}