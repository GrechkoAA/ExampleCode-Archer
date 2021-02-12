using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Transform _target;
    [SerializeField] private UnityEngine.Events.UnityEvent Turned;
    [SerializeField] private UnityEngine.Events.UnityEvent Destroyed;


    private int _currentHealt;

    public event System.Action<int, Vector3> ReceivedDamage;

    public int Healpt => _health;

    private void Start()
    {
        _currentHealt = _health;
    }

    public void AppyDamage(int damage, Vector3 position)
    {
        _currentHealt -= damage;

        ReceivedDamage?.Invoke(GetPoint(position), _target.position);
        Turned?.Invoke();

        if (_currentHealt <= 0)
        {
            Destroyed?.Invoke();
        }
    }

    private int GetPoint(Vector3 position)
    {
        float maxDistance = 0.1f;
        float distantion = Vector2.Distance(_target.position, position);

        return distantion < maxDistance ? 10 : 5;
    }
}