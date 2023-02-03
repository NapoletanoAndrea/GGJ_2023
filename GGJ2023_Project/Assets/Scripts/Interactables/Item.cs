using UnityEngine;

public abstract class Item : ScriptableObject
{
	public GameObject handPrefab;
	public bool loseOnUse;
	public abstract void Use();
}