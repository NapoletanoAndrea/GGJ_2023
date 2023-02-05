using System.Collections;
using TMPro;
using UnityEngine;

public class TextFadeComponent : MonoBehaviour
{
	public TMP_Text text;

	public void CompleteFade(float fadeSeconds, float stayOnScreenSeconds)
	{
		StartCoroutine(CompleteFadeCoroutine(fadeSeconds, stayOnScreenSeconds));
	}

	private IEnumerator FadeCoroutine(float fadeSeconds, float startAlpha, float endAlpha)
	{
		text.color = text.color.GetAlphaColor(startAlpha);

		float t = 0;
		
		while (t < 1)
		{
			t += Time.deltaTime / fadeSeconds;
			text.color = text.color.GetAlphaColor(Mathf.Lerp(startAlpha, endAlpha, t));
			yield return null;
		}

		text.color = text.color.GetAlphaColor(endAlpha);
	}

	private IEnumerator CompleteFadeCoroutine(float fadeSeconds, float stayOnScreenSeconds)
	{
		yield return FadeCoroutine(fadeSeconds, 0, 1);
		yield return new WaitForSeconds(stayOnScreenSeconds);
		yield return FadeCoroutine(fadeSeconds, 1, 0);
	}
}