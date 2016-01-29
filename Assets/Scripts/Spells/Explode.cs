using UnityEngine;

public class Explode : SpellEffect
{
	public float damage = 10;

	public override void HitSomething(Collision col)
	{
		var player = col.transform.GetComponent<PlayerController>();
		player.health -= damage;
	}

	public override bool IsValid(Collision col)
	{
		return (col.transform.tag == "Player");
	}
}
