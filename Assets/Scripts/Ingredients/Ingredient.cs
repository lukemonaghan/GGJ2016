using UnityEngine;

public class Ingredient : MonoBehaviour
{
	bool triggerEnabled = true;
	public Sprite sprite;
	public GameParameters.IngredientTypes type;
	public Color color;

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

			spellController.AddColor(color);
            
			UIManager.Instance.inGameMenu.AddIngredient(transform, sprite, type);

			Camera.main.GetComponent<CameraController>().Shake(Vector2.one * 0.1f, 0.05f);

			// Animation Hook goes here
			Destroy(gameObject);
		}
	}
}
