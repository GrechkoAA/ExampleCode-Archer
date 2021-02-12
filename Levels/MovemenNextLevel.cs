using UnityEngine;
using DG.Tweening;

public class MovemenNextLevel : MonoBehaviour
{
    [SerializeField] private Level _level;

    [Header("Options")]
    [SerializeField] private float _transitionDelayInSeconds;
    [SerializeField] private float _transitionSpeedInSeconds;
    [SerializeField] private bool _isCamera;

    private float _distantion = 30;

    public static MovementState State;

    private void Move()
    {
        State = MovementState.Active;

        DOTween.Sequence().AppendInterval(_transitionDelayInSeconds)
                          .AppendCallback(() =>
                          {
                              transform.DOMoveZ(transform.position.z + _distantion, _transitionSpeedInSeconds)
                                       .SetDelay(_transitionDelayInSeconds)
                                       .OnComplete(() =>
                                       {
                                           if (_isCamera == true)
                                           {
                                               State = MovementState.Inactive;
                                           }
                                       });
                          });
    }

    private void OnEnable()
    {
        _level.Completed += Move;
    }
}