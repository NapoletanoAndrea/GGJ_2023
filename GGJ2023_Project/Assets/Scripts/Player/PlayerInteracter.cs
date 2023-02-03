using UnityEngine;

public class PlayerInteracter : MonoBehaviour
{
    public KeyCode interactKey;
    public LayerMask interactLayer;
    public float distance;

    private Transform camTransform;

    private Collider currentCollider;
    private IHoverable currentHoverable;

    private void Awake()
    {
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Physics.Raycast(camTransform.position, camTransform.forward, out var hit,
                distance, interactLayer))
        {
            if (hit.collider != currentCollider)
            {
                currentHoverable?.OnExitHover();
                currentCollider = hit.collider;
                currentHoverable = hit.collider.gameObject.GetComponent<IHoverable>();
                currentHoverable?.OnEnterHover();
            }
            else
            {
                currentHoverable?.OnHover();
            }
            
            if (Input.GetKeyDown(interactKey))
            {
                IClickable clickable = hit.collider.gameObject.GetComponent<IClickable>();
                clickable?.OnClick();
            }
        }
    }
}
