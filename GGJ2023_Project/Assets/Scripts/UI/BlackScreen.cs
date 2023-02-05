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

	private void InvokeInEvent()
	{
		FadedIn?.Invoke();
	}

	private void InvokeOutEvent()
	{
		FadedOut?.Invoke();
	}

	public void SetColor(Color color)
	{
		float alpha = blackScreen.color.a;
		blackScreen.color = color.GetAlphaColor(alpha);
	}

	public void SetAlpha(float alpha)
	{
		blackScreen.color = blackScreen.color.GetAlphaColor(alpha);
	}

	public void FadeIn(float seconds)
	{
		StartCoroutine(FadeCoroutine(seconds, 0, 1, InvokeInEvent));
	}

	public IEnumerator FadeCoroutine(float seconds, float startAlpha, float endAlpha, Action action = null)
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
		StartCoroutine(FadeCoroutine(seconds, 1, 0, InvokeOutEvent));
	}

	public void CompleteFade(float fadeSeconds, float stayOnScreenSeconds)
	{
		StartCoroutine(CompleteFadeCoroutine(fadeSeconds, stayOnScreenSeconds));
	}

	private IEnumerator CompleteFadeCoroutine(float fadeSeconds, float stayOnScreenSeconds)
	{
		yield return FadeCoroutine(fadeSeconds, 0, 1, InvokeInEvent);
		yield return new WaitForSeconds(stayOnScreenSeconds);
		yield return FadeCoroutine(fadeSeconds, 1, 0, InvokeOutEvent);
	}
}