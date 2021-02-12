using DG.Tweening;
using UnityEngine;

public class UpwardMovementTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Min(0)] private float _delay;
    [SerializeField, Min(0)] private float _stopDelay;

    Sequence movement;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        movement = DOTween.Sequence();
        movement.Append(_target.transform.DOMoveY(GetTopPosition(), _delay)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        movement.AppendInterval(_stopDelay);
    }

    public void OnStopAnimation()
    {
        movement.Kill();
    }

    private float GetTopPosition()
    {
        var stepUp = 0.17f;

        return transform.localScale.x * stepUp + transform.position.y;
    }
}