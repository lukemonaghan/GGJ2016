using UnityEngine;

public class IngredientSpawnPoint : MonoBehaviour
{
	private void Start()
	{
		var ingredients = GameParameters.Instance.Ingredients;
		Instantiate(ingredients[Random.Range(0, ingredients.Length)], transform.position + Vector3.one, Quaternion.identity);
	}
}
