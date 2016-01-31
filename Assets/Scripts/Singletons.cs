using UnityEngine;

[DisallowMultipleComponent]
public class SingleMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance
	{
		get
		{
			if (__instance__ == null)
				__instance__ = FindObjectOfType<T>();
			if (__instance__ == null)
				__instance__ = new GameObject(typeof(T).Name).AddComponent<T>();
			return __instance__;
		}
		protected set { __instance__ = value; }
	}
	private static T __instance__;
}