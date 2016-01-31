using UnityEngine;

public class RotateObject : MonoBehaviour
{
	public Vector3 axis;

	void Update ()
	{
		transform.Rotate(axis * Time.deltaTime);
	}
}
