using UnityEngine;

public class Killbox : MonoBehaviour
{
	public void OnTriggerEnter(Collider c)
	{
		Destroy(c.gameObject);
	}
}
