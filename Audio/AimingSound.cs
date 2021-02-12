using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Weapon))]
public class AimingSound : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        _weapon.Aimed += _audioSource.Play;
        _weapon.Shooting += (_) => _audioSource.Stop();
    }
}
