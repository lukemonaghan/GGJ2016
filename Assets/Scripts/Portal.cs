using UnityEngine;

public class Portal : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			Destroy(col.gameObject);
			UIManager.Instance.SetActiveAll(false);
			UIManager.Instance.mainMenu.gameObject.SetActive(true);
		}
	}
}
