using UnityEngine;
using UnityEngine.Events;

public class OnPortalsReachedEventBehavior : MonoBehaviour
{
	private int portalsNeeded = 2;

	private int count = 0;

	public Animator ani;

	public UnityEvent toRaise;

	private PlayerMovement playerMovement;

	private void Awake()
	{
		playerMovement = FindObjectOfType<PlayerMovement>();
	}

	private void OnEnable()
	{
		playerMovement.OnPlayerDeath += Activate;
	}

	private void OnDisable()
	{
		playerMovement.OnPlayerDeath -= Activate;
	}

	private void Activate()
	{
		count++;
		if (count == portalsNeeded)
		{
            if (ani)
            {
				ani.SetBool("IsOpen", true);
			}
			toRaise?.Invoke();
		}
	}
}