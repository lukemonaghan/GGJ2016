﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
	public GameObject defaultObject;

	public void OnEnable()
	{
		Invoke("DelaySelect",0.1f);
	}

	public void DelaySelect()
	{
		if (EventSystem.current != null)
		{
			EventSystem.current.SetSelectedGameObject(defaultObject);
		}
	}

	public void Play()
	{
		SceneManager.LoadScene("Main");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
