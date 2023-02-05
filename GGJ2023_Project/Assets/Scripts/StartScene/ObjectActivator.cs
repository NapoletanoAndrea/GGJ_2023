using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
	[Min(1)] public int deathsNeeded = 1;
	public bool disableAfterActivation;
	public List<GameObject> toActivate;
	public List<GameObject> toDeactivate;

	private int deathCount;

	private PlayerMovement pMove;

	private void Awake()
	{
		pMove = FindObjectOfType<PlayerMovement>();
	}

	private void OnEnable()
	{
		pMove.OnPlayerDeath += TryActivate;
	}

	private void OnDisable()
	{
		pMove.OnPlayerDeath -= TryActivate;
	}

	private void TryActivate()
	{
		deathCount++;
		if (deathCount == deathsNeeded)
		{
			foreach (var obj in toActivate)
			{
				obj.SetActive(true);
			}
			foreach (var obj in toDeactivate)
			{
				obj.SetActive(false);
			}
			if (disableAfterActivation)
			{
				enabled = false;
			}
		}
	}
}