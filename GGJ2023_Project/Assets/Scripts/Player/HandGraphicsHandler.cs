using UnityEngine;

public class HandGraphicsHandler : MonoBehaviour
{
	[SerializeField] private Transform itemSpot;
	
	private PlayerHand playerHand;
	private GameObject itemHandInstance;

	private void Awake()
	{
		playerHand = FindObjectOfType<PlayerHand>();
	}

	private void OnEnable()
	{
		playerHand.OnItemPickedUp += OnItemPickedUp;
		playerHand.OnItemDiscarded += OnItemDiscarded;
	}

	private void OnDisable()
	{
		playerHand.OnItemPickedUp -= OnItemPickedUp;
		playerHand.OnItemDiscarded -= OnItemDiscarded;
	}

	private void OnItemPickedUp()
	{
		if (playerHand.HeldItem.handPrefab)
		{
			itemHandInstance = Instantiate(playerHand.HeldItem.handPrefab, itemSpot);
		}
	}

	private void OnItemDiscarded()
	{
		Destroy(itemHandInstance);
	}
}
