using UnityEngine;
using System.Collections;

public class ExplosionCreator : MonoBehaviour 
{
	public GameObject explosionPrefab;
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			GameObject.Instantiate(explosionPrefab,Vector3.zero,Quaternion.identity);
	}
}
