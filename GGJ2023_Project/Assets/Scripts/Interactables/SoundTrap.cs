using System.Collections.Generic;
using UnityEngine;

public class SoundTrap : MonoBehaviour
{
	private PlayerMovement playerMovement;
	private Chaser chaser;

	[SerializeField] private Collider openDoorTrigger;
	[SerializeField] private float openSeconds;

	private List<Door> doors = new();

	private void Awake()
	{
		chaser = FindObjectOfType<Chaser>();
		playerMovement = FindObjectOfType<PlayerMovement>();
		
		var results = Physics.OverlapSphere(openDoorTrigger.bounds.center, openDoorTrigger.bounds.extents.x);
		foreach (var result in results)
		{
			Door door = result.GetComponent<Door>();
			if (door)
			{
				doors.Add(door);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (chaser && !playerMovement.isSlow)
			{
				Debug.Log("Lured");
				chaser.hasHeardSound = true;
				chaser.agent.SetDestination(transform.position);
				foreach (var door in doors)
				{
					door.OpenTimed(openSeconds);
				}
			}	
		}
	}
}