using UnityEngine;

public class BulletAnimation : MonoBehaviour
{
    [SerializeField] private WeaponAnimations _weaponAnimation;
    [SerializeField] private float _shotDelay;

    private enum WeaponAnimations
    {
        isBow,
        isThrow01,
        isThrow02,
        isThrow03
    };

    public string AnimationName => _weaponAnimation.ToString();

    public float ShotDelay => _shotDelay;
}