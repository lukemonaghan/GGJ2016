using UnityEngine;

public class IngredientSpawnPoint : MonoBehaviour
{
	void Start()
	{
		SpawnIngredient();
	}

	public void SpawnIngredient()
	{
		var ingredients = GameParameters.Instance.Ingredients;
		Instantiate(ingredients[Random.Range(0, ingredients.Length)], transform.position + Vector3.one, Quaternion.identity);
	}
}
