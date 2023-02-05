using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
	public TextFadeComponent textFadeComponent;

	public float waitSeconds;
	public float screenFadeOut;
	public float textFadeSeconds;
	public float textStayOnScreen;

	private PlayerMovement playerMovement;
	
	private void Start()
	{
		playerMovement = FindObjectOfType<PlayerMovement>();
		playerMovement.enabled = false;
		BlackScreen.Instance.SetColor(Color.black);
		BlackScreen.Instance.SetAlpha(1);
		textFadeComponent.CompleteFade(textFadeSeconds, textStayOnScreen);
		Invoke(nameof(FadeOut), waitSeconds);
	}

	private void FadeOut()
	{
		BlackScreen.Instance.FadeOut(screenFadeOut);
		BlackScreen.Instance.FadedOut += OnFadedOut;
	}

	private void OnFadedOut()
	{
		playerMovement.enabled = true;
	}
}