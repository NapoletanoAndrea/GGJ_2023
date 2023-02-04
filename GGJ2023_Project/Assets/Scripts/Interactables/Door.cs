using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    
    [SerializeField] private float openSpeed = 1;
    [Min(0)] public int layout = 0;
    
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = openSpeed;
    }

    public void Open()
    {
        CancelInvoke();
        animator.SetBool(IsOpen, true);
    }

    public void OpenTimed(float seconds)
    {
        Open();
        Invoke(nameof(Close), seconds);
    }

    public void Close()
    {
        CancelInvoke();
        animator.SetBool(IsOpen, false);
    }
}
