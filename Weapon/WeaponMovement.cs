using DG.Tweening;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private float _speed;

    private Tween _movement;
    private float _currentTime;
    private Vector3[] _trajectory;

    public event System.Action<GameObject> Stopped;

    public void Init(Transform[] points)
    {
        _trajectory = GetTrajectory(points);

        StartMoveing();
    }

    private void StartMoveing()
    {
        _movement = DOTween.To(() => _currentTime, time => _currentTime = time, 1, GetDuration())
                           .SetEase(Ease.Linear)
                           .OnUpdate(Move);
    }

    private float GetDuration()
    {
        return Vector3.Distance(_trajectory[0], _trajectory[3]) / _speed;
    }

    private Vector3[] GetTrajectory(Transform[] points)
    {
        var trajectory = new Vector3[points.Length];

        for (int i = 0; i < trajectory.Length; i++)
        {
            trajectory[i] = points[i].position;
        }

        return trajectory;
    }

    private void Move()
    {
        transform.position = Bezier.GetPoint(_trajectory[0], _trajectory[1], _trajectory[2], _trajectory[3], _currentTime);
        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(_trajectory[0], _trajectory[1], _trajectory[2], _trajectory[3], _currentTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        _movement.Kill();

        if (other.GetComponentInParent<Health>() is Health health)
        {
            Stopped?.Invoke(health.gameObject);
        }
        else
        {
            Stopped?.Invoke(other.gameObject);
        }
    }
}