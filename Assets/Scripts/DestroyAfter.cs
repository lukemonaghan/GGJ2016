using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
	public float timer = 0;

	void Start ()
	{
		var particle = GetComponentInChildren<ParticleSystem>();
		if (particle != null)
			timer = particle.duration;

		Invoke("Destroy",timer);
	}

	void Destroy()
	{
		Destroy(gameObject);
	}
}
