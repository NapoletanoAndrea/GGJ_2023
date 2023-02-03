using System;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	private Item heldItem = null;
	public Item HeldItem => heldItem;

	public event Action OnItemPickedUp;
	public event Action OnItemDiscarded;
	public event Action OnItemUsed;

	private void Start()
	{
		if (heldItem)
		{
			OnItemPickedUp?.Invoke();
		}
	}

	public bool IsEmpty()
	{
		return heldItem == null;
	}

	public void Pickup(Item item)
	{
		heldItem = item;
		OnItemPickedUp?.Invoke();
	}

	public void UseHeldItem()
	{
		if (heldItem)
		{
			heldItem.Use();
			OnItemUsed?.Invoke();
			if (heldItem.loseOnUse)
			{
				DiscardHeldItem();
			}
		}
	}

	public void DiscardHeldItem()
	{
		heldItem = null;
		OnItemDiscarded?.Invoke();
	}
}