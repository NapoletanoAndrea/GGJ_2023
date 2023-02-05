using System.Collections.Generic;
using UnityEngine;

public class SoundTrap : MonoBehaviour
{
	private PlayerMovement playerMovement;
	private Chaser chaser;

	[SerializeField] private bool dontAttractRoots = false;
	[SerializeField] private Collider openDoorTrigger;
	[SerializeField] private float openSeconds;

	private List<Door> doors = new();

	private GenericEntityAudio audio;

	private void Awake()
	{
		audio = GetComponent<GenericEntityAudio>();
		chaser = FindObjectOfType<Chaser>();
		playerMovement = FindObjectOfType<PlayerMovement>();
		
		var results = Physics.OverlapSphere(openDoorTrigger.bounds.center, openDoorTrigger.bounds.extents.x);
		foreach (var result in results)
		{
			if (result.transform.parent)
			{
				Door door = result.transform.parent.GetComponent<Door>();
				if (door)
				{
					doors.AddUnique(door);
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (chaser && !playerMovement.isSlow)
			{
				if (!dontAttractRoots)
				{
					chaser.hasHeardSound = true;
					chaser.agent.SetDestination(transform.position);	
				}
				foreach (var door in doors)
				{
					door.OpenTimed(openSeconds);
				}
				audio?.PlayAudio("Trigger");
			}	
		}
	}
}