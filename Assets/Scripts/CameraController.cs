using UnityEngine;
public class CameraController : MonoBehaviour
{
	public Transform target;
	private Vector3 offset;
	public float speed = 5;
	public void Start()
	{
		offset = transform.position - target.transform.position;
	}

	public void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position,target.transform.position + offset,Time.deltaTime * speed);
	}
}
