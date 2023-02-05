using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private float fadeSeconds;
	[SerializeField] private float stayOnScreenSeconds;
	[SerializeField] private float fadeDialogueSeconds;
	[SerializeField] private float dialogueStayOnScreenSeconds;

	public TextFadeComponent textFadeComponent;

	private PlayerMovement playerMovement;
	private Chaser chaser;

	private void Awake()
	{
		playerMovement = FindObjectOfType<PlayerMovement>();
		chaser = FindObjectOfType<Chaser>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			BlackScreen.Instance.SetColor(Color.white);
			BlackScreen.Instance.CompleteFade(fadeSeconds, stayOnScreenSeconds);
			BlackScreen.Instance.FadedIn += OnFadedIn;
			playerMovement.enabled = false;
			if (chaser)
			{
				chaser.enabled = false;
				chaser.agent.isStopped = true;
			}
		}
	}

	private void OnFadedIn()
	{
		BlackScreen.Instance.FadedIn -= OnFadedIn;
		BlackScreen.Instance.FadedOut += OnFadedOut;
		textFadeComponent.CompleteFade(fadeDialogueSeconds, dialogueStayOnScreenSeconds);
		playerMovement.RestorePosition();
	}

	private void OnFadedOut()
	{
		BlackScreen.Instance.FadedOut -= OnFadedOut;
		playerMovement.enabled = true;
		if (chaser)
		{
			chaser.enabled = true;
			chaser.agent.isStopped = false;
		}
		enabled = false;
	}
}