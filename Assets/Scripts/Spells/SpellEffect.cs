using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
	public float power = 1;

	public Renderer renderer { get { return _renderer ?? (_renderer = GetComponentInChildren<Renderer>()); } }
	private Renderer _renderer;
	public ParticleSystem[] particleSystems { get { return _particleSystems ?? (_particleSystems = GetComponentsInChildren<ParticleSystem>()); } }
	private ParticleSystem[] _particleSystems;
	
	public Color color
	{
		set
		{
			var explode = this as Explode;
            if (explode != null)
			{
				explode.peakColor = value;
			}
			if (renderer != null && renderer.material.HasProperty("_Color"))
			{
				renderer.material.SetColor("_Color", value);
			}
			foreach (var p in particleSystems)
			{
				p.startColor = value;
			}
		}
		get
		{
			if (renderer != null && renderer.material.HasProperty("_Color"))
			{
				return renderer.material.GetColor("_Color");
			}
			foreach (var p in particleSystems)
			{
				return p.startColor;
			}
			return Color.black;
		}
	}
}
