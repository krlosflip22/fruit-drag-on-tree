using UnityEngine;
using System.Collections;

public class FruitScaleAnimator : MonoBehaviour
{
	private Transform cachedTransform;

	void Awake()
	{
		cachedTransform = transform;
	}

	public void PlayPop()
	{
		StopAllCoroutines();
		StartCoroutine(ScaleRoutine(1.2f, 0.1f));
	}

	public void PlayBounce()
	{
		StopAllCoroutines();
		StartCoroutine(ScaleRoutine(0.8f, 0.1f));
	}

	private IEnumerator ScaleRoutine(float targetScaleFactor, float duration)
	{
		Vector3 originalScale = cachedTransform.localScale;
		Vector3 targetScale = originalScale * targetScaleFactor;

		float timer = 0f;
		while (timer < duration)
		{
			cachedTransform.localScale = Vector3.Lerp(originalScale, targetScale, timer / duration);
			timer += Time.deltaTime;
			yield return null;
		}
		cachedTransform.localScale = targetScale;

		timer = 0f;
		while (timer < duration)
		{
			cachedTransform.localScale = Vector3.Lerp(targetScale, originalScale, timer / duration);
			timer += Time.deltaTime;
			yield return null;
		}
		cachedTransform.localScale = originalScale;
	}
}
