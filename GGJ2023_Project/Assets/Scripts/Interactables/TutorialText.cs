using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
	public TMP_Text text;

	private void Awake()
	{
		text.enabled = false;
	}
	
}