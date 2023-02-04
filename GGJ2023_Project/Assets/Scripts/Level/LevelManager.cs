using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private float levelHeight;
	[SerializeField] private Transform center;

	[ReadOnly] public Transform[] intersectionPoints;

	[Button]
	private void GenerateIntersectionPoints()
	{
		
	}
}