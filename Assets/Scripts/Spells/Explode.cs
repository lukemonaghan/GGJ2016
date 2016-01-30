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

	void Start()
	{
		childLight = GetComponentInChildren<Light>();
		Camera.main.GetComponent<CameraController>().Shake(Vector2.one * 0.05f * power, loopduration * 0.75f);
	}

	public void Update()
	{
		transform.localScale += Vector3.one * maxSize * power * (1/loopduration) * Time.deltaTime;
		UpdateMesh();
	}

	public void OnCollisionEnter(Collision col)
	{
		var entity = col.transform.GetComponent<Entity>();
		if (entity != null)
		{
			var amount = damage*power;
            entity.health -= amount;
			if ((entity is PlayerController) == false)
			{
				UIManager.Instance.inGameMenu.AddScore(transform,amount);
            }
			else
			{
				UIManager.Instance.inGameMenu.DamagePopup(transform,(int)-amount);
            }
		}
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
		{
			Destroy(gameObject);
		}

		var r = Mathf.Sin((currentTime / loopduration			    ) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		var g = Mathf.Sin((currentTime / loopduration + 0.33333333f ) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		var b = Mathf.Sin((currentTime / loopduration + 0.66666667f ) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		var correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;

		renderer.material.SetVector("_ChannelFactor", new Vector4(r, g, b, 0));
		renderer.material.SetFloat("_ClipRange", loopduration - currentTime);
		renderer.material.SetColor("_Color", childLight.color);
	}
}
