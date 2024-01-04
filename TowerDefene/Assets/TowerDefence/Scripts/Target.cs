using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	[SerializeField] private int _health = 1000;
	public void OnDamage(int damage)
	{
		_health -= damage;
	}
}