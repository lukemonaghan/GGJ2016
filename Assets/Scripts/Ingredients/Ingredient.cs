using UnityEngine;

public class Ingredient : MonoBehaviour
{
	bool triggerEnabled = true;
	public Sprite sprite;
	public GameParameters.IngredientTypes type;

	public Renderer renderer { get { return _renderer ?? (_renderer = GetComponentInChildren<Renderer>()); } }
	private Renderer _renderer;

	public void OnTriggerEnter(Collider c)
	{
		if (triggerEnabled == false)
			return;

		if (c.tag == "Player")
		{
			triggerEnabled = false;

			var spellController = c.transform.GetComponent<SpellController>();

			spellController.AddColor(renderer.material.color);
            
			UIManager.Instance.inGameMenu.AddIngredient(sprite, type);

			// Animation Hook goes here
			Destroy(gameObject);
		}
	}
}
