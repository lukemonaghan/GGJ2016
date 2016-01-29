using UnityEngine;

public class Ingredient : MonoBehaviour
{
	bool triggerEnabled = true;
	public Sprite sprite;
	public GameParameters.IngredientTypes type;

	public void OnTriggerEnter(Collider c)
	{
		if (triggerEnabled == false)
			return;

		if (c.tag == "Player")
		{
			triggerEnabled = false;
            
			UIManager.Instance.inGameMenu.AddIngredient(sprite, type);

			// Animation Hook goes here
			Destroy(gameObject);
		}
	}
}
