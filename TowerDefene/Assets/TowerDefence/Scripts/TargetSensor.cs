using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TargetSensor : MonoBehaviour
{
	public delegate void TargetEnterEvent(Transform target);
	public delegate void TargetExitEvent(Vector3 lastKnownPosition);

	public event TargetEnterEvent OnTargetEnter;
	public event TargetExitEvent OnTargetExit;


	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out Target target))
		{
			OnTargetEnter?.Invoke(target.transform);
			Debug.Log("target enter");
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.TryGetComponent(out Target target))
		{
			OnTargetExit?.Invoke(target.transform.position);
			Debug.Log("target exit");
		}
	}
}