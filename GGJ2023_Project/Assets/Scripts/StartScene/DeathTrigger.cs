using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
	private PlayerMovement player;
	
	[SerializeField] private float fadeSeconds;

	private void Awake()
	{
		player = FindObjectOfType<PlayerMovement>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			BlackScreen.Instance.SetColor(Color.black);
			BlackScreen.Instance.FadeIn(fadeSeconds);
			BlackScreen.Instance.FadedIn += OnFadedIn;
		}
	}

	private void OnFadedIn()
	{
		BlackScreen.Instance.FadedIn -= OnFadedIn;
		player.RestorePosition();
		BlackScreen.Instance.FadeOut(fadeSeconds);
	}
}