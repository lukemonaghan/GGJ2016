using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public float speed = 5;

	public VignetteAndChromaticAberration chromatic;
	public Bloom bloom;
	public GameObject book;

	float chromaticDefaultValue; 
	float bloomDefaultValue;

	public void Start()
	{
		if (Math.Abs(offset.magnitude) < 0.1f)
		{
			offset = transform.position - target.transform.position;
		}
		transform.position = target.transform.position + offset;
		chromaticDefaultValue = chromatic.chromaticAberration;
		bloomDefaultValue = bloom.bloomIntensity;
	}

	public void LateUpdate()
	{

		if (target == null)
		{
			return;
		}

		transform.position = Vector3.Lerp(transform.position,target.transform.position + offset,Time.deltaTime * speed);
	}

	public void Shake(Vector2 amount,float time)
	{
		StartCoroutine(ScreenShake(amount,time));
	}

	private IEnumerator ScreenShake(Vector2 amount, float time)
	{
        while (time > 0)
		{
			transform.position += new Vector3(Random.Range(-1.0f, 1.0f) * amount.x, Random.Range(-1.0f, 1.0f) * amount.y,0);
			chromatic.chromaticAberration += Random.Range(-20f, 20f);
			bloom.bloomIntensity += Random.Range(-0.1f,0.2f);
			time -= Time.deltaTime;
			yield return null;
		}
		chromatic.chromaticAberration = chromaticDefaultValue;
		bloom.bloomIntensity = bloomDefaultValue;
	}
}
