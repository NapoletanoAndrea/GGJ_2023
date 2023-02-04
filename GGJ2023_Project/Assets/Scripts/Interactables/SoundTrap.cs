using UnityEngine;

public class SoundTrap : MonoBehaviour
{
	private Chaser chaser;
	
	private void Awake()
	{
		chaser = FindObjectOfType<Chaser>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (chaser)
			{
				chaser.hasHeardSound = true;
			}	
		}
	}
}