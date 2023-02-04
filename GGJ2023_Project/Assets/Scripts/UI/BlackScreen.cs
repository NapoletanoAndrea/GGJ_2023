using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
	public static BlackScreen Instance;
	
	private RawImage blackScreen;

	public event Action FadedIn;
	public event Action FadedOut;

	private void Awake()
	{
		blackScreen = GetComponent<RawImage>();
		if (Instance)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	public void FadeIn(float seconds)
	{
		StartCoroutine(FadeCoroutine(seconds, 0, 1, FadedIn));
	}

	private IEnumerator FadeCoroutine(float seconds, float startAlpha, float endAlpha, Action action = null)
	{
		blackScreen.color = blackScreen.color.GetAlphaColor(startAlpha);

		float t = 0;
		
		while (t < 1)
		{
			t += Time.deltaTime / seconds;
			blackScreen.color = blackScreen.color.GetAlphaColor(Mathf.Lerp(startAlpha, endAlpha, t));
			yield return null;
		}

		blackScreen.color = blackScreen.color.GetAlphaColor(endAlpha);
		action?.Invoke();
	}

	public void FadeOut(float seconds)
	{
		StartCoroutine(FadeCoroutine(seconds, 1, 0, FadedOut));
	}
}