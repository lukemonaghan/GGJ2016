using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[SerializeField] private float _health;
	public float health
	{
		get { return _health; }
		set { _health = value; if (OnHealthChanged != null) OnHealthChanged(_health); if (_health <= 0) { OnDeath(); } }
	}

	protected Action<float> OnHealthChanged;

	public GameObject[] deathBits;

	public abstract void OnDeath();
}
