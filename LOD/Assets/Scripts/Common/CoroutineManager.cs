using UnityEngine;
using System.Collections;

public static class CoroutineManager {
	private static MonoBehaviour mainBehavior;

	public static Coroutine StartCoroutine(IEnumerator routine)
	{
		return mainBehavior.StartCoroutine (routine);
	}

	public static void Init(GameObject go)
	{
		mainBehavior =  go.GetComponent<MonoBehaviour> ();
	}
}
