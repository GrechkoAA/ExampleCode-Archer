using DG.Tweening;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Tween rotation;

    public void Rotation()
    {
        rotation = transform.DOLocalRotate(new Vector3(0, -360, 0), _delay, RotateMode.LocalAxisAdd)
                            .SetLoops(-1, LoopType.Incremental)
                            .SetEase(Ease.Linear);
    }

    public void Stop()
    {
        rotation.Kill();
    }
}
