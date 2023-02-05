using UnityEngine;
using UnityEngine.SceneManagement;

public class RootTrigger : MonoBehaviour
{
	public int buildIndex;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(buildIndex);
		}
	}
}