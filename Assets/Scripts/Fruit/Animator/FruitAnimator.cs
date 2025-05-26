using UnityEngine;

public class FruitAnimator : MonoBehaviour
{
    [SerializeField] private FruitScaleAnimator scaleAnimator;
    public FruitScaleAnimator Scale => scaleAnimator;

    [SerializeField] private FruitJiggleAnimator jiggleAnimator;
    public FruitJiggleAnimator Jiggle => jiggleAnimator;
}
