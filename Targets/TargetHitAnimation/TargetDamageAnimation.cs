using DG.Tweening;
using UnityEngine;

public class TargetDamageAnimation : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private Vector3 _rotationAxis;
    [SerializeField] private Ease _ease;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private bool _isAnimationCurver;

    public void PlayHitAnimation()
    {
        if (_isAnimationCurver)
        {
            _pivot.DOLocalRotate(_rotationAxis, 0.65f, RotateMode.LocalAxisAdd)
              .SetEase(animationCurve);
        }
        else
        {
            _pivot.DOLocalRotate(_rotationAxis, 1, RotateMode.LocalAxisAdd)
                  .SetEase(_ease);
        }
    }
}