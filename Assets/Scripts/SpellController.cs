using UnityEngine;

public class SpellController : MonoBehaviour
{
	public void Update()
	{
		if (Mathf.Abs(Input.GetAxisRaw("A_1")) > 0.01f)
		{
			UIManager.Instance.inGameMenu.ActivateSpell();
		}
	}
}
