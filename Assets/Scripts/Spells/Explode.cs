using UnityEngine;

// [URL] https://stevencraeynest.wordpress.com/2013/03/29/easy-volumetric-explosion-in-unity3d/
public class Explode : SpellEffect
{
	public float damage = 10;

	public float maxSize = 3.0f;
	public float loopduration = 1.0f;
	public float rangeModifyer = 2.0f;

	public Color endColor = Color.black;
	public Color peakColor = Color.red;

	Light childLight;
	float currentTime = 0.0f;


	public Renderer renderer { get { return _renderer ?? (_renderer = GetComponentInChildren<Renderer>()); } }
	private Renderer _renderer;

	void Start()
	{
		childLight = GetComponentInChildren<Light>();
		loopduration += Random.value;
	}

	public void Update()
	{
		transform.localScale += Vector3.one * maxSize * Time.deltaTime;
		UpdateMesh();
	}

	public void OnCollisionEnter(Collision col)
	{
		Debug.Log(col.transform.name);
		var entity = col.transform.GetComponent<Entity>();
		if (entity == null)
		{
			return;
		}
		entity.health -= damage * power;
		Destroy(gameObject);
	}

	void UpdateMesh()
	{
		childLight.range += currentTime * rangeModifyer;

		if (currentTime > loopduration * 0.5f)
			childLight.color = Color.Lerp(peakColor, endColor, (loopduration * 0.5f) + (currentTime * 0.5f));
		else
			childLight.color = Color.Lerp(endColor, peakColor, (currentTime * 2.0f));

		currentTime += Time.deltaTime;
		if (currentTime >= loopduration)
			Destroy(gameObject);

		var r = Mathf.Sin((Time.time / loopduration) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		var g = Mathf.Sin((Time.time / loopduration + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		var b = Mathf.Sin((Time.time / loopduration + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		var correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;

		renderer.material.SetVector("_ChannelFactor", new Vector4(r, g, b, 0));
		renderer.material.SetFloat("_ClipRange", loopduration - currentTime);
		renderer.material.SetColor("_ExplosionColor", childLight.color);
	}
}
