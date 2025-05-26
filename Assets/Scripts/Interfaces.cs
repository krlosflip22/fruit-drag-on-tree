using UnityEngine;

/// <summary>
/// Defines draggable behavior. Any object that can be dragged implements this.
/// </summary>
public interface IDraggable
{
    void OnBeginDrag(Vector2 pointerPosition);
    void OnDrag(Vector2 pointerPosition);
    void OnEndDrag(Vector2 pointerPosition);
}

/// <summary>
/// Spawner interface for spawning fruits at specific points.
/// </summary>
public interface IFruitSpawner
{
    void SpawnFruitAt(Transform point);
}