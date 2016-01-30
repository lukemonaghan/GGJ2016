using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIWorldPopup : MonoBehaviour
{

	public void EnableAtPosition(Vector3 position)
	{
		gameObject.SetActive(true);

		var canvasRect = UIManager.Instance.transform as RectTransform;
		Vector2 screenPoint = Camera.main.WorldToViewportPoint(position);
		screenPoint = new Vector2
		(
			((screenPoint.x * canvasRect.sizeDelta.x)),
			((screenPoint.y * canvasRect.sizeDelta.y))
		);

		var resPopRectTrans = transform as RectTransform;
		resPopRectTrans.position = screenPoint + (Random.insideUnitCircle * 64.0f);

		transform.localScale = Vector3.zero;
		StartCoroutine(Scale(Vector3.one, 25));
	}

	public void MoveTowards(Vector2 position,float speed = 10,Action onComplete = null)
	{
		StartCoroutine(Move(position, speed, onComplete));
	}

	IEnumerator Scale(Vector3 scale, float speed)
	{
		var rectTrans = transform as RectTransform;
		while (Vector2.Distance(scale, rectTrans.localScale) > 0.1f)
		{
			rectTrans.localScale = Vector2.Lerp(rectTrans.localScale, scale, speed * Time.deltaTime);
			yield return null;
		}
		rectTrans.localScale = scale;
	}

	IEnumerator Move(Vector2 position, float speed, Action onComplete)
	{
		var rectTrans = transform as RectTransform;
		while (Vector2.Distance(position, rectTrans.position) > 0.1f)
		{
			rectTrans.position = Vector2.Lerp(rectTrans.position, position,speed * Time.deltaTime);
			yield return null;
		}
		rectTrans.position = position;
		if (onComplete != null)
		{
			onComplete.Invoke();
		}
	}
}
