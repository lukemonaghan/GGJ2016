using UnityEngine;
using System.Collections;

// [URL] https://stevencraeynest.wordpress.com/2013/03/29/easy-volumetric-explosion-in-unity3d/
public class Explosion : MonoBehaviour 
{
	public float loopduration = 1.0f;
	public float rangeModifyer = 2.0f;
	
	public Color endColor = Color.black;
	public Color peakColor = Color.red;
	
	Light childLight;
	float currentTime = 0.0f;

	void Start()
	{
		childLight = GetComponentInChildren<Light>();
		loopduration += Random.value;
	}
	
	void Update () 
	{	
		transform.localScale = new Vector3(currentTime,currentTime,currentTime) * 10;
		childLight.range += currentTime * rangeModifyer;
		
		if (currentTime > loopduration * 0.5f)
			childLight.color = Color.Lerp(peakColor,endColor,(loopduration * 0.5f) + (currentTime * 0.5f));
		else
			childLight.color = Color.Lerp(endColor,peakColor,(currentTime * 2.0f));
		
		currentTime += Time.deltaTime;
		if (currentTime >= loopduration)
			Destroy(gameObject);
			
		float r = Mathf.Sin((Time.time / loopduration) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin((Time.time / loopduration + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin((Time.time / loopduration + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;
		
		GetComponent<Renderer>().material.SetVector("_ChannelFactor", new Vector4(r,g,b,0));
		GetComponent<Renderer>().material.SetFloat("_ClipRange",loopduration - currentTime);
	}
}
