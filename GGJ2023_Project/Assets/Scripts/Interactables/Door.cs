using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        CancelInvoke();
        animator.SetBool(IsOpen, true);
    }

    public void OpenTimed(float seconds)
    {
        Open();
        Invoke(nameof(seconds), seconds);
    }

    public void Close()
    {
        CancelInvoke();
        animator.SetBool(IsOpen, false);
    }
}
