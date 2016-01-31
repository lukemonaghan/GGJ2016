using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public GameObject splash;
	public UIMain mainMenu;
	public UIInGame inGameMenu;
	public UIEndState endState;

	public static UIManager Instance;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		SetActiveAll(false);

		// Main scene is always first. :P 
		switch (SceneManager.GetActiveScene().buildIndex)
		{
			case 0:
				splash.SetActive(true);
				Invoke("LoadMenu",2.0f);
				break;
			case 1:
			mainMenu.gameObject.SetActive(true);
				break;
			default:
				inGameMenu.gameObject.SetActive(true);
				break;
		}
	}

	void LoadMenu()
	{
		SceneManager.LoadScene(1);
	}

	public void SetActiveAll(bool enabled)
	{
		mainMenu.gameObject.SetActive(enabled);
		inGameMenu.gameObject.SetActive(enabled);
		endState.gameObject.SetActive(enabled);
		splash.SetActive(enabled);
	}

	// DEBUG

	public IngredientSpawnPoint[] spawnPoints { get { return _spawnPoints ?? (_spawnPoints = FindObjectsOfType<IngredientSpawnPoint>()); } }
	private IngredientSpawnPoint[] _spawnPoints;

	public void Update()
	{
		DebugSpawn();
		DebugGodMode();
	}

	private bool wasPressedStart = false;
	private void DebugSpawn()
	{
		var Start_1 = Input.GetAxisRaw("Start_1");
		if (Mathf.Abs(Start_1) > 0.01f && wasPressedStart == false)
		{
			wasPressedStart = true;

			// Clear the old ones. Else lags.
			var ings = FindObjectsOfType<Ingredient>();
			foreach (var i in ings)
			{
				Destroy(i.gameObject);
			}

			// Spawn new ones
			foreach (var s in spawnPoints)
			{
				s.SpawnIngredient();
			}
		}
		if (Mathf.Abs(Start_1) < 0.01f && wasPressedStart == true)
		{
			wasPressedStart = false;
		}
	}

	public PlayerController player { get { return _player ?? (_player = FindObjectOfType<PlayerController>()); } }
	private PlayerController _player;

	private bool wasPressedBack = false;
	private void DebugGodMode()
	{
		var Back_1 = Input.GetAxisRaw("Back_1");
		if (Mathf.Abs(Back_1) > 0.01f && wasPressedBack == false)
		{
			wasPressedBack = true;
			player.health += 100000;
			inGameMenu.DamagePopup(player.transform, 100000);
		}
		if (Mathf.Abs(Back_1) < 0.01f && wasPressedBack == true)
		{
			wasPressedBack = false;
		}
	}
}
