using UnityEngine;

public class ItemWorld : MonoBehaviour, IClickable, IHoverable
{
	public Item item;
	
	private PlayerHand playerHand;

	private void Awake()
	{
		playerHand = FindObjectOfType<PlayerHand>();
	}

	public void OnClick()
	{
		if (playerHand.IsEmpty())
		{
			playerHand.Pickup(item);
			Destroy(gameObject);
		}
	}

	public void OnEnterHover()
	{
		
	}

	public void OnExitHover()
	{
		
	}

	public void OnHover()
	{
		Debugger.DrawSphere(transform.position, 1, 0, Color.cyan);
	}
}