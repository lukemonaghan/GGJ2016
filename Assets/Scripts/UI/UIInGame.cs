using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
	public List<Image> uiIngredientBar = new List<Image>();
	public Dictionary<GameParameters.IngredientTypes,int> currentTypes = new Dictionary<GameParameters.IngredientTypes, int>();
	 
	public void AddIngredient(Sprite spriteImage,GameParameters.IngredientTypes type)
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

	void ClearIngredients()
	{
		foreach (var ingredient in uiIngredientBar)
		{
			ingredient.sprite = null;
			ingredient.gameObject.SetActive(false);
		}
		currentTypes.Clear();
    }

	public void ActivateSpell()
	{
		GameParameters.Spell CurrentSpell = null;
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
			if (didPass)
			{
				CurrentSpell = spell;
				break;
			}
		}

		if (CurrentSpell == null)
		{
			return;
		}
		var controller = FindObjectOfType<PlayerController>();
		var obj = Instantiate(CurrentSpell.effect);
		obj.transform.position = controller.transform.position + controller.transform.forward;

		ClearIngredients();
	}

}
