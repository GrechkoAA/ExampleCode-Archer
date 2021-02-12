using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private WeaponInput _weaponInput;
    [SerializeField] private CrosshairMovement _crosshair;
    [SerializeField] private WeaponTrajectory _trajectory;

    private WeaponMovement _bulletTempate;
    private bool _isSightActive;
    private bool _canMovingTrajectory;
    private bool _canFire = true;
    private bool _isTrajectoryActive;
    private float _shotDelay;
    private bool _isFirstShot;

    public event System.Action<WeaponMovement, bool> Shooted;
    public event System.Action<bool> Shooting;
    public event System.Action Aimed;
    public event System.Action CanShooted;

    public float DistanceCrosshair { get => Vector3.Distance(_crosshair.transform.position, _trajectory.Points[0].position); }

    private void Start()
    {
        MovemenNextLevel.State = MovementState.Inactive;
    }

    public void Update()
    {
        Aim();
    }

    public void Init(WeaponMovement weaponMovement, float shotDelay)
    {
        _bulletTempate = weaponMovement;
        _shotDelay = shotDelay;
    }

    private void Aim()
    {
        if (_canMovingTrajectory == true)
        {
            _isSightActive = IsSightActive();

            if (_isSightActive == true)
            {
                _canMovingTrajectory = false;
                _crosshair.gameObject.SetActive(true);
                _trajectory.Update();
                _crosshair.SetPosition(_trajectory.HitTarget);
            }
            else
            {
                _crosshair.enabled = false;
                _crosshair.gameObject.SetActive(false);

                _trajectory.Disable();
            }
        }
    }

    private void Shoot()
    {
        WeaponMovement newWeapon = Instantiate(_bulletTempate, Vector3.zero, Quaternion.identity);
        newWeapon.transform.localScale /= 2;
        newWeapon.Init(_trajectory.Points);
        Shooted?.Invoke(newWeapon, _trajectory.IsTarget);
        newWeapon.GetComponent<AudioSource>().enabled = true;

        if (newWeapon.TryGetComponent(out WeaponRotation weaponRotation))
        {
            weaponRotation.Rotation();
        }

        newWeapon.Stopped += (other) =>
        {
            _level.UpdateTargetList(other);


            weaponRotation?.Stop();
        };


        DOTween.Sequence().AppendInterval(_shotDelay)
                          .AppendCallback(() =>
                          {
                              _canFire = true;
                              CanShooted?.Invoke();
                          }
                          );


        _isFirstShot = true;
    }

    private bool IsSightActive()
    {
        var minimumDistance = 2.3f;

        return _trajectory.Points[3].position.z - _trajectory.Points[0].position.z > minimumDistance;
    }

    private void OnEnable()
    {
        _weaponInput.ConstructedTrajectory += (aimPoint) =>
        {
            if (CanBuildTrajectory() == true)
            {
                Aimed?.Invoke();

                _trajectory.Activate(aimPoint);
                _isTrajectoryActive = true;
            }
        };

        _weaponInput.MovedTrajectory += (aimPoint) =>
        {
            if (CanMoveTrajectory() == true)
            {
                _canMovingTrajectory = true;
                _trajectory.Move(aimPoint);
            }
        };

        _weaponInput.DisabledTrajectory += () =>
        {
            DisableSight();

            if (IsSightActive() == false && CanBuildTrajectory())
            {
                Shooting?.Invoke(false);
            }
            else if (CanShot() == true)
            {
                Shooting?.Invoke(true);
                ShootWithDelay();
            }
            else if (_isFirstShot == false)
            {
                Shooting?.Invoke(false);
            }
        };
    }

    private void ShootWithDelay()
    {
        Shoot();

        _canFire = false;
        _isTrajectoryActive = false;
    }

    private void DisableSight()
    {
        _trajectory.Disable();
        _crosshair.gameObject.SetActive(false);
        _canMovingTrajectory = false;
    }

    private bool CanShot()
    {
        return (_isSightActive == true) && (_isTrajectoryActive == true);
    }

    private bool CanMoveTrajectory()
    {
        return _isTrajectoryActive == true && Time.timeScale != 0;
    }

    private bool CanBuildTrajectory()
    {
        return (MovemenNextLevel.State == MovementState.Inactive) && (_canFire == true);
    }
}