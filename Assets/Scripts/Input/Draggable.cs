using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour, IDraggable
{
    [SerializeField] private Transform cachedTransform;

    private bool isDragging;
    private Vector3 dragOffset;

    public System.Action<Vector2> OnBeginDragEvent;
    public System.Action<Vector2> OnEndDragEvent;

    void Awake()
    {
        if (cachedTransform == null) cachedTransform = transform;
    }

    void OnMouseDown()
    {
        Vector2 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        OnBeginDrag(pointerPos);
    }

    void OnMouseDrag()
    {
        Vector2 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        OnDrag(pointerPos);
    }

    void OnMouseUp()
    {
        Vector2 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        OnEndDrag(pointerPos);
    }

    public void OnBeginDrag(Vector2 pointerPosition)
    {
        isDragging = true;
        dragOffset = (Vector3)pointerPosition - cachedTransform.position;

        OnBeginDragEvent?.Invoke(pointerPosition);
    }

    public void OnDrag(Vector2 pointerPosition)
    {
        if (!isDragging) return;
        cachedTransform.position = (Vector2)pointerPosition - (Vector2)dragOffset;
    }

    public void OnEndDrag(Vector2 pointerPosition)
    {
        isDragging = false;

        OnEndDragEvent?.Invoke(pointerPosition);
    }
}
