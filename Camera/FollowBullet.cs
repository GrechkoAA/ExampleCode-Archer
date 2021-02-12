using DG.Tweening;
using UnityEngine;

public class FollowBullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Camera _targetCamera;
    [Header("Options")]
    [SerializeField] private Vector3 _offsetTargetCamera;
    [SerializeField, Min(0)] private float _trackingPercentage;
    [SerializeField, Range(0, 20)] private float _minimumSensingDistance;
    [SerializeField, Min(0)] private float _timeScale;
    [SerializeField, Min(0)] private float _cameraDelayNearTarget;
    [SerializeField] private LayerMask _characterMask;

    private Transform _bulletPosition;
    private Vector3 _originDistantion;
    private Vector3 _originPosition;
    private Vector3 _originRotation;

    public event System.Action<bool> IsDelayed;
    public event System.Action Stoped;

    private void LateUpdate()
    {
        if (_bulletPosition != null)
        {
            transform.position = _bulletPosition.position + _originDistantion;
        }
    }

    private void Follow(WeaponMovement weaponMovement, bool isTarget)
    {
        if (isTarget == false)
        {
            return;
        }

        if (IsFollow() == true)
        {
            Time.timeScale = _timeScale;
            IsDelayed?.Invoke(true);

            SaveOriginPosition();
            SetPosition(weaponMovement);

            DisableTracking(weaponMovement);
            _targetCamera.cullingMask &= ~_characterMask;
        }
    }

    private void DisableTracking(WeaponMovement weaponMovement)
    {
        weaponMovement.Stopped += (other) =>
        {
            Stoped?.Invoke();

            _targetCamera.cullingMask |= _characterMask;
            _bulletPosition = null;
            
            DOTween.Sequence().AppendInterval(GetNormalizedSeconds(_cameraDelayNearTarget)).AppendCallback
            (() =>
             {
                 Time.timeScale = 1f;
                 IsDelayed?.Invoke(false);

                 ResetPosition();
                 SaveOriginPosition();
             });
        };
    }

    private float GetNormalizedSeconds(float cameraDelayNearTarget)
    {
        var normalizeSeconds = 1 / Time.timeScale;

        return cameraDelayNearTarget / normalizeSeconds;
    }

    private void SaveOriginPosition()
    {
        _originPosition = transform.position;
        _originRotation = transform.rotation.eulerAngles;
    }

    private void SetPosition(WeaponMovement weaponMovement)
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _originDistantion = Vector3.zero - _offsetTargetCamera;
        _bulletPosition = weaponMovement.transform;
    }

    private void ResetPosition()
    {
        _bulletPosition = null;

        transform.position = _originPosition;
        transform.rotation = Quaternion.Euler(_originRotation);
    }

    private bool IsFollow()
    {
        bool isMinDistance = true;
        bool isFollow = true;

        if (_weapon.DistanceCrosshair <= _minimumSensingDistance)
        {
            isMinDistance = false;
        }

        if (Random.Range(1, 101) > _trackingPercentage)
        {
            isFollow = false;
        }

        return isMinDistance && isFollow;
    }

    private void OnEnable()
    {
        _weapon.Shooted += (weaponMovement, isTarget) => Follow(weaponMovement, isTarget);
    }
}