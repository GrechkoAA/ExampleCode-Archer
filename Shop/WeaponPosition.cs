using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _rotation;

    public void SetTransform()
    {
        transform.localPosition = _position;
        transform.localRotation = Quaternion.Euler(_rotation);
    }
}
