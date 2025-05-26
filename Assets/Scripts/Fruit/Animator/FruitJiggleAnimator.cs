using UnityEngine;

public class FruitJiggleAnimator : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform fruitTransform;
    [SerializeField] private float maxRotation = 20f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private float scaleAmount = 0.1f;
    [SerializeField] private float scaleSpeed = 10f;

    private Vector3 lastParentPosition;
    private Vector3 initialPivotScale;

    private bool jiggling = false;

    void Start()
    {
        if (pivot == null)
        {
            Debug.LogError("JiggleJoint2D: Pivot not assigned.");
            enabled = false;
            return;
        }

        lastParentPosition = transform.position;
        initialPivotScale = pivot.localScale;
    }

    public void EnableJiggle(Vector2 pointerPosition)
    {
        jiggling = true;
        transform.parent = null;
        transform.position = pointerPosition;
        fruitTransform.parent = pivot;
    }

    public void DisableJiggle(Vector2 pointerPosition)
    {
        jiggling = false;
        fruitTransform.parent = null;
        transform.parent = fruitTransform;

        fruitTransform.localEulerAngles = Vector3.zero;
    }

    void Update()
    {
        if (!jiggling) return;

        Vector3 parentDelta = transform.position - lastParentPosition;
        float speed = parentDelta.magnitude / Time.deltaTime;

        // Calculate direction angle (in 2D, we only care about X movement)
        float targetAngle = Mathf.Clamp(-parentDelta.x * maxRotation, -maxRotation, maxRotation);

        // Smoothly rotate pivot around Z axis (since it's 2D)
        float currentZ = pivot.localEulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f; // Convert to -180..180
        float newZ = Mathf.Lerp(currentZ, targetAngle, rotationSpeed * Time.deltaTime);
        pivot.localEulerAngles = new Vector3(0f, 0f, newZ);

        // Apply scale jiggle based on speed
        float scaleFactor = 1f + Mathf.Clamp(speed * 0.01f, 0f, scaleAmount);
        Vector3 targetScale = new Vector3(initialPivotScale.x, initialPivotScale.y * scaleFactor, initialPivotScale.z);
        pivot.localScale = Vector3.Lerp(pivot.localScale, targetScale, scaleSpeed * Time.deltaTime);

        // Update last position
        lastParentPosition = transform.position;
    }
}
