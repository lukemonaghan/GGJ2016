using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public float speed = 5;
	public void Start()
	{
		if (Math.Abs(offset.magnitude) < 0.1f)
		{
			offset = transform.position - target.transform.position;
		}
		transform.position = target.transform.position + offset;
	}

	public void LateUpdate()
	{

		if (target == null)
		{
			return;
		}

		transform.position = Vector3.Lerp(transform.position,target.transform.position + offset,Time.deltaTime * speed);
	}
}
