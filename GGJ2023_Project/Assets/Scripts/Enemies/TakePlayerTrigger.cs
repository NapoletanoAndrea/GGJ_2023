using UnityEngine;

public class TakePlayerTrigger : MonoBehaviour
{
	[SerializeField] private float fadeSeconds;

	private Chaser chaser;
	private DoorManager doorManager;
	private PlayerMovement playerMovement;

	private void Awake()
	{
		chaser = FindObjectOfType<Chaser>();
		doorManager = FindObjectOfType<DoorManager>();
		playerMovement = FindObjectOfType<PlayerMovement>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			chaser.enabled = false;
			chaser.agent.isStopped = true;
			playerMovement.enabled = false;
			BlackScreen.Instance.FadeIn(fadeSeconds);
			BlackScreen.Instance.FadedIn += OnFadedIn;
		}
	}

	private void OnFadedIn()
	{
		BlackScreen.Instance.FadedOut += OnFadedOut;
		BlackScreen.Instance.FadeOut(fadeSeconds);
		chaser.RestoreInitialPosition();
		chaser.enabled = true;
		doorManager.ChangeLayout();
	}

	private void OnFadedOut()
	{
		BlackScreen.Instance.FadedOut -= OnFadedOut;
		playerMovement.enabled = true;
	}
}