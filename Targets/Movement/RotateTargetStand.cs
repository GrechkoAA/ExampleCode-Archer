using DG.Tweening;
using UnityEngine;

public class RotateTargetStand : MonoBehaviour
{
    [SerializeField, Min(0)] private float _delay;
    [SerializeField] private Ease _ease;
    [SerializeField] private int _countTargets;

    private int _angleRotation;
    private int _numberTurns;

    private void Start()
    {
        _angleRotation = 360 / _countTargets;
    }

    public void OnTurn()
    {
        _numberTurns++;

        if (_countTargets != _numberTurns)
        {
            transform.DOLocalRotate(new Vector3(0, _angleRotation, 0), _delay, RotateMode.LocalAxisAdd).SetEase(_ease);
        }
    }
}