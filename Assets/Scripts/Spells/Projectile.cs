using UnityEngine;

public class Projectile : SpellEffect
{
	public float damage = 10;
	public float speed = 25;

	void Start()
	{
		Camera.main.GetComponent<CameraController>().Shake(Vector2.one * 0.05f * power, 0.2f);
	}

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
				UIManager.Instance.inGameMenu.AddScore(transform,amount);
			}
		}
		var hitEffect = Instantiate(GameParameters.Instance.Projectile_Hit, col.contacts[0].point + (col.contacts[0].normal * 0.1f), Quaternion.LookRotation(col.contacts[0].normal)) as GameObject;
		foreach (var ps in hitEffect.GetComponentsInChildren<ParticleSystem>())
		{
			ps.startColor = color;
		}
		Destroy(gameObject);
	}
}
