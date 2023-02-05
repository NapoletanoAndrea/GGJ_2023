using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTrigger : MonoBehaviour
{
	public float fadeSeconds;
	public int buildIndex;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			BlackScreen.Instance.SetColor(Color.black);
			StartCoroutine(BlackScreen.Instance.FadeCoroutine(fadeSeconds, 0, 1, LoadScene));
		}
	}

	private void LoadScene()
	{
		SceneManager.LoadScene(buildIndex);
	}
}