using UnityEngine;

public class Enemy : Entity
{
	public float damage = 10;

    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Player")
		{
			OnDeath();
		}
	}

	public override void OnDeath()
	{
		Destroy(gameObject);

		for (var i = 0; i < deathBits.Length; i++)
		{
			Instantiate(deathBits[i], transform.position, transform.rotation);
		}
	}
}
