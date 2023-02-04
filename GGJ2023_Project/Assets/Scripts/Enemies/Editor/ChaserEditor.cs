using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Chaser))]
public class ChaserEditor : Editor
{
	private void OnSceneGUI()
	{
		Chaser chaser = (Chaser) target;
		Transform transform = chaser.transform;
		
		Handles.color = Color.white;
		Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, chaser.maxDistanceFromPlayer);
		
		Handles.color = Color.red;
		Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, chaser.aggroRange);
	}
}
