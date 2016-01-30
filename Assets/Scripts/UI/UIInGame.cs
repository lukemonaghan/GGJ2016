using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
	public List<Image> uiIngredientBar = new List<Image>();
	public Dictionary<GameParameters.IngredientTypes,int> currentTypes = new Dictionary<GameParameters.IngredientTypes, int>();

	private float _score = 0.0f;

	public float score
	{
		set
		{
			_score = value;
			scoreText.text = ((int) _score).ToString();
		}
		get { return _score; }
	}

	public Text scoreText;

	public void AddIngredient(Sprite spriteImage, GameParameters.IngredientTypes type)
	{
		// Object Pooling
		GameObject newIngredient = null;
		foreach (var ing in uiIngredientBar)
		{
			// Have we found the next disabled element
			if (ing.gameObject.activeSelf == false)
			{
				// Set it active
				ing.gameObject.SetActive(true);

				// Set our reference
				newIngredient = ing.gameObject;
				break;
			}
		}

		// didnt find one in the pool. Add a new one for us. Duplicating the first(Always should exist)
		if (newIngredient == null)
		{
			// Make one
			newIngredient = Instantiate(uiIngredientBar[0].gameObject);

			// Parent it
			var trans = newIngredient.transform as RectTransform;
			trans.SetParent(uiIngredientBar[0].transform.parent);

			// Move it
			var lastTrans = uiIngredientBar[uiIngredientBar.Count - 1].transform as RectTransform;
			trans.anchoredPosition = lastTrans.anchoredPosition + new Vector2(lastTrans.sizeDelta.x * 0.75f,0);

			// Add it
			uiIngredientBar.Add(newIngredient.GetComponent<Image>());
        }

		// Add our type
		if (currentTypes.ContainsKey(type))
		{
			currentTypes[type] += 1;
		}
		else
		{
			currentTypes.Add(type, 1);
		}

		// Set out sprite
		newIngredient.GetComponent<Image>().sprite = spriteImage;
	}

	private void FixIngredients()
	{
		// Clear the images. (Turn them off)
		foreach (var ingredient in uiIngredientBar)
		{
			ingredient.sprite = null;
			ingredient.gameObject.SetActive(false);
		}
		// Clear our actual spell stack
		currentTypes.Clear();
	}

	public GameParameters.SpellType ActivateSpell(out GameObject newObject, Color color)
	{
		newObject = null;
		// Get a list of all avaliable spells
		var CurrentSpells = new List<GameParameters.Spell>();
		foreach (var spell in GameParameters.Instance.Spells)
		{
			var didPass = true;
			foreach (var ing in spell.ingredients)
			{
				if (currentTypes.ContainsKey(ing.type) == false)
				{
					didPass = false;
				}
				else
				{
					if (ing.minCount > currentTypes[ing.type])
					{
						didPass = false;
					}
				}
			}
			// If we have all the requiered ingredients, add it.
			if (didPass)
			{
				CurrentSpells.Add(spell);
			}
		}

		// Did we have any applicable spells?
		if (CurrentSpells.Count == 0)
		{
			return GameParameters.SpellType.NONE;
		}

		// Sort them.
		CurrentSpells.Sort(SortSpells);

		// Snag the best spell
		var CurrentSpell = CurrentSpells[0];

		// Grab our player
		var controller = FindObjectOfType<PlayerController>();

		SpellEffect effectPrefab = null;
		// Do the correct animation
		switch (CurrentSpell.type)
		{
			case GameParameters.SpellType.Explode:
				effectPrefab = GameParameters.Instance.Explosion;
                break;
			case GameParameters.SpellType.Projectile:
				effectPrefab = GameParameters.Instance.Projectile;
				break;
		}

		// Create the spell effect
		newObject = Instantiate(effectPrefab.gameObject);
		newObject.transform.position = controller.transform.position + controller.transform.forward;
		newObject.transform.rotation = controller.transform.rotation;
		var effect = newObject.GetComponent<SpellEffect>();
		effect.color = color;

		// Add its power
		foreach (var s in CurrentSpell.ingredients)
		{
			effect.power += currentTypes[s.type];
		}

		// Clear our stack
		FixIngredients();

		return CurrentSpell.type;
	}

	// Basic sort function
	private int SortSpells(GameParameters.Spell s1, GameParameters.Spell s2)
	{
		var s1n = s1.ingredients.Sum(s => s.minCount);
		var s2n = s2.ingredients.Sum(s => s.minCount);

		if (s1n < s2n) return 1;
		if (s1n > s2n) return -1;
		return 0;
	}
}
