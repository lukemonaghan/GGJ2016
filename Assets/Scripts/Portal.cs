using UnityEngine;

public class Portal : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			Destroy(col.gameObject);
			UIManager.Instance.SetActiveAll(false);
			UIManager.Instance.endState.gameObject.SetActive(true);
			UIManager.Instance.endState.DisplayInfo("Congratulations!\n You escaped the witches dungeon.");
		}
	}
}
