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
			float amount = damage * power;
			entity.health -= amount;
			if ((entity is PlayerController) == false)
			{
				UIManager.Instance.inGameMenu.score += amount;
			}
		}
		Destroy(gameObject);
		var hitEffect = Instantiate(GameParameters.Instance.Projectile_Hit, col.contacts[0].point + (col.contacts[0].normal * 0.1f), Quaternion.LookRotation(col.contacts[0].normal)) as GameObject;
		foreach (var ps in hitEffect.GetComponentsInChildren<ParticleSystem>())
		{
			ps.startColor = color;
		}
	}
}
