using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[SerializeField] private float _health;
	public float health
	{
		get { return _health; }
		set { _health = value; if (_health <= 0) { OnDeath(); } }
	}

	public GameObject[] deathBits;

	public abstract void OnDeath();
}
