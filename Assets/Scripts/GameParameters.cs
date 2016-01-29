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



}
