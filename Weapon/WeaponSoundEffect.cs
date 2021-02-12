using UnityEngine;

public class WeaponSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _shoot;

    void Start()
    {
        _shoot.Play();
    }
}