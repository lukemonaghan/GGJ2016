using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{

	public abstract void HitSomething(Collision col);
	public abstract bool IsValid(Collision col);

	public void OnCollisionEnter(Collision col)
	{
		if (IsValid(col))
		{
			HitSomething(col);
		}
	}
}
