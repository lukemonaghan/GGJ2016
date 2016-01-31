using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEndState : MonoBehaviour
{
	public Text infoText;
	public GameObject defaultObject;

	public void OnEnable()
	{
		Invoke("DelaySelect", 0.1f);
	}

	public void DelaySelect()
	{
		if (EventSystem.current != null)
		{
			EventSystem.current.SetSelectedGameObject(defaultObject);
		}
	}

	public void DisplayInfo(string info)
	{
		infoText.text = info;
	}

	public void Restart()
	{
		SceneManager.LoadScene("Main");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
