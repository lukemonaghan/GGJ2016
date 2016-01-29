using UnityEngine;

public class UIManager : MonoBehaviour
{

	public static UIManager Instance;

	public UIMain mainMenu;
	public UIInGame inGameMenu;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
        }
		Instance = this;
	}

	void Start()
	{
		SetActiveAll(false);
		inGameMenu.gameObject.SetActive(true);
	}

	public void SetActiveAll(bool enabled)
	{
		mainMenu.gameObject.SetActive(enabled);
		inGameMenu.gameObject.SetActive(enabled);
	}
}
