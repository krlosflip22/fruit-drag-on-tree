using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Concrete factory that creates fruit instances from prefabs using an object pool.
/// Assign the fruit prefab and sprite list in the Inspector.
/// </summary>
public class FruitFactory : MonoBehaviour
{
	[SerializeField] private Fruit fruitPrefab;
	[SerializeField] private Sprite[] fruitSprites;
	[SerializeField] private int poolSize = 10;

	private Queue<Fruit> fruitPool = new Queue<Fruit>();
	private List<Fruit> activeFruits = new List<Fruit>();

	void Awake()
	{
		// Pre-instantiate pool
		for (int i = 0; i < poolSize; i++)
		{
			Fruit fruit = Instantiate(fruitPrefab, transform);
			fruit.gameObject.SetActive(false);
			fruitPool.Enqueue(fruit);
		}
	}

	/// <summary>
	/// Create or reuse a random fruit at the spawn point.
	/// </summary>
	public GameObject CreateRandomFruit(Transform spawnPoint)
	{
		Fruit fruit;

		if (fruitPool.Count > 0)
		{
			fruit = fruitPool.Dequeue();
		}
		else
		{
			// Reuse oldest active fruit
			fruit = activeFruits[0];
			activeFruits.RemoveAt(0);
			ResetFruit(fruit);
		}

		// Setup fruit
		fruit.transform.position = spawnPoint.position;
		Sprite spriteToSpawn = fruitSprites[Random.Range(0, fruitSprites.Length)];
		fruit.SetSprite(spriteToSpawn);
		fruit.name = spriteToSpawn.name;

		fruit.gameObject.SetActive(true);
		activeFruits.Add(fruit);

		return fruit.gameObject;
	}

	/// <summary>
	/// Disable all active fruits and return them to the pool.
	/// </summary>
	public void ResetAllFruits()
	{
		foreach (var fruit in activeFruits)
		{
			ResetFruit(fruit);
			fruitPool.Enqueue(fruit);
		}

		activeFruits.Clear();
	}

	/// <summary>
	/// Clean up and disable a fruit.
	/// </summary>
	private void ResetFruit(Fruit fruit)
	{
		fruit.gameObject.SetActive(false);
		fruit.Reset();
	}
}
