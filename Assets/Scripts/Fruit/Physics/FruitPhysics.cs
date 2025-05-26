using UnityEngine;

public class FruitPhysics : MonoBehaviour
{
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float groundY = -3.5f;
    [SerializeField] private float bounceFactor = 0.5f;
    [SerializeField] private float velocityThreshold = 0.1f;

    private Vector2 velocity;
    private bool isGrounded;
    private bool isSimulating = false;

    private Transform cachedTransform;

    void Awake()
    {
        cachedTransform = transform;
    }

    void Update()
    {
        if (!isSimulating || isGrounded) return;

        velocity.y += gravity * Time.deltaTime;
        cachedTransform.position += (Vector3)(velocity * Time.deltaTime);

        if (cachedTransform.position.y <= groundY)
        {
            cachedTransform.position = new Vector3(cachedTransform.position.x, groundY, cachedTransform.position.z);

            if (Mathf.Abs(velocity.y) > velocityThreshold)
            {
                velocity.y = -velocity.y * bounceFactor;
            }
            else
            {
                velocity = Vector2.zero;
                isGrounded = true;
            }
        }
    }

    public void Stop()
    {
        isSimulating = false;
        velocity = Vector2.zero;
    }

    public void Resume()
    {
        isSimulating = true;
        isGrounded = false;
    }
}
