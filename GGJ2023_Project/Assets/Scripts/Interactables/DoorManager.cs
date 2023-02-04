using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
	private Door[] doors;
	public int currentLayout;
	[ReadOnly] public int maxLayout;

	private Door[] prevLayoutDoors = Array.Empty<Door>();

	private List<Door[]> doorsList = new();

	private void Awake()
	{
		doors = FindObjectsOfType<Door>();

		Door[] currentLayoutDoors;
		int currLayout = 0;

		do
		{
			currentLayoutDoors = doors.Where(door => door.layout == currLayout).ToArray();
			doorsList.Add(currentLayoutDoors);
			maxLayout = currLayout;
			currLayout++;

		} while (currentLayoutDoors.Length > 0);
	}

	private void Start()
	{
		UpdateDoors();
	}

	public void ChangeLayout()
	{
		currentLayout++;
		if (currentLayout > maxLayout)
		{
			currentLayout = 0;
		}
		UpdateDoors();
	}

	private void UpdateDoors()
	{
		foreach (var door in prevLayoutDoors)
		{
			door.Close();
		}
		foreach (var door in doorsList[currentLayout])
		{
			door.Open();
		}
		prevLayoutDoors = doorsList[currentLayout];
	}

}