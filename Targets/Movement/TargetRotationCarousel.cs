using DG.Tweening;
using UnityEngine;

public class TargetRotationCarousel : MonoBehaviour
{
    [SerializeField, Range(0, 2)] private float _delay;
    [SerializeField] private Vector3 _axis;

    void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        var rotationAngle = 360f;

        transform.DORotate(GetRotationAxis(rotationAngle), _delay)
                 .SetLoops(-1, LoopType.Incremental)
                 .SetEase(Ease.Linear);
    }

    private Vector3 GetRotationAxis(float rotationAngle)
    {
        return new Vector3(_axis.x > 0 ? rotationAngle : transform.rotation.eulerAngles.x,
                           _axis.y > 0 ? rotationAngle : transform.rotation.eulerAngles.y,
                           _axis.z > 0 ? rotationAngle : transform.rotation.eulerAngles.z);
    }
}