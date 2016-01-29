using UnityEngine;

public class Projectile : SpellEffect
{
	public float damage = 10;
	public float speed = 25;

	public void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	public void OnCollisionEnter(Collision col)
	{
		var entity = col.transform.GetComponent<Entity>();
		if (entity != null)
		{
			entity.health -= damage * power;
		}
		Destroy(gameObject);
	}
}
