using UnityEngine;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(FruitAnimator))]
[RequireComponent(typeof(FruitPhysics))]
public class Fruit : MonoBehaviour
{
    private Transform spawnPoint;
    private IFruitSpawner spawner;

    private Draggable draggable;

    private FruitAnimator animator;
    private FruitPhysics physics;

    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;

    void Awake()
    {
        draggable = GetComponent<Draggable>();

        animator = GetComponent<FruitAnimator>();
        physics = GetComponent<FruitPhysics>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSortingOrder = spriteRenderer.sortingOrder;
        }

        draggable.OnBeginDragEvent += HandleBeginDrag;
        draggable.OnEndDragEvent += HandleEndDrag;
    }

    public void SetSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        spriteRenderer.sprite = sprite;
    }

    public void SetSpawnPoint(Transform point)
    {
        spawnPoint = point;
    }

    public void SetSpawner(IFruitSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void HandleBeginDrag(Vector2 pointerPosition)
    {
        if (spawnPoint != null && spawner != null)
        {
            spawner.SpawnFruitAt(spawnPoint);
            spawnPoint = null;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = originalSortingOrder + 1;
        }

        animator.Scale.PlayPop();
        animator.Jiggle.EnableJiggle(pointerPosition);
    }

    private void HandleEndDrag(Vector2 pointerPosition)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = originalSortingOrder;
        }

        animator.Scale.PlayBounce();
        animator.Jiggle.DisableJiggle(pointerPosition);

        physics.Resume();
    }

    public void Reset()
    {
        physics.Stop();

        animator.Jiggle.DisableJiggle(Vector2.zero);
    }
}
