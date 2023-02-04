using TMPro;
using UnityEngine;

public enum TriggerCondition
{
    Enter,
    Stay,
    Exit
}

public class TextTrigger : MonoBehaviour
{
    [SerializeField, TextArea] private string text;
    
    [SerializeField] private TriggerCondition triggerCondition;
    [SerializeField] private bool disableText;

    private TMP_Text tmpText;

    private void Awake()
    {
        tmpText = FindObjectOfType<TutorialText>().text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerCondition == TriggerCondition.Enter)
        {
            ChangeText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerCondition == TriggerCondition.Exit)
        {
			ChangeText();
        }
    }

    private void ChangeText()
    {
        tmpText.enabled = true;
        tmpText.text = text;
        if (disableText)
        {
            tmpText.enabled = false;
        }
    }
}
