using UnityEngine;

public class TakePlayerTrigger : MonoBehaviour
{
	private DoorManager doorManager;

	private void Awake()
	{
		doorManager = FindObjectOfType<DoorManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			doorManager.ChangeLayout();
		}
	}
}