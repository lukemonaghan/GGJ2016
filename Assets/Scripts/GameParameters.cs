using System;
using System.Collections.Generic;
using UnityEngine;

/// A place to store miscellaneous global parameters.
public class GameParameters : ScriptableObject 
{
    // Convenience method for accessing GameParameters asset
    public static GameParameters Instance
    {
        get
        {
            if (_instance == null)
                _instance = (GameParameters)Resources.Load("GameParameters", typeof(GameParameters));
            return _instance;
        }
    }
    static GameParameters _instance;

	public Ingredient[] Ingredients;

	[Serializable]
	public enum IngredientTypes
	{
		Type1,
		Type2,
		Type3,
		Type4,
	}

	[Serializable]
	public class Spell
	{
		[Serializable]
		public struct IngredientCount
		{
			public IngredientTypes type;
			public int minCount;
		}

		public IngredientCount[] ingredients;
		public SpellEffect effect;
	}

	public List<Spell> Spells = new List<Spell>();

	[RuntimeInitializeOnLoadMethod()]
	public static void CreateLevel()
	{
		Debug.Log("Bootstrap");
	}
}
