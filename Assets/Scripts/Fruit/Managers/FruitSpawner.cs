using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The Spawner (attached to the Tree GameObject) handles initial spawning and respawning of fruits.
/// Uses IFruitFactory to decouple the spawner from specific fruit prefabs.
/// </summary>
public class FruitSpawner : MonoBehaviour, IFruitSpawner
{
    [Tooltip("FruitFactory component (set in Inspector)")]
    public FruitFactory fruitFactory;

    [Tooltip("Spawn points (empty Transforms) on the tree where fruits appear.")]
    public List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        InitialSpawn();
    }

    void InitialSpawn()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnFruitAt(spawnPoint);
        }
    }

    /// <summary>
    /// Spawns a random fruit at the given spawn point.
    /// This can be called both at Start and when a fruit is picked.
    /// </summary>
    public void SpawnFruitAt(Transform spawnPoint)
    {
        GameObject fruitObj = fruitFactory.CreateRandomFruit(spawnPoint);
        if (fruitObj == null) return;

        // Initialize the new fruit's script with references.
        Fruit fruitComp = fruitObj.GetComponent<Fruit>();
        if (fruitComp != null)
        {
            fruitComp.SetSpawnPoint(spawnPoint);
            fruitComp.SetSpawner(this);
        }
        else
        {
            Debug.LogWarning("Spawned object has no Fruit component: " + fruitObj.name);
        }
    }

    public void Reset()
    {
        fruitFactory.ResetAllFruits();
        InitialSpawn();
    }
}
