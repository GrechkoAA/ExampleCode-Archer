using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(FollowBullet))]
public class SoundProjectileFlying : MonoBehaviour
{
    [SerializeField] private AudioSource  _audioSource;
    [SerializeField] private FollowBullet _followBullet;

    private void OnEnable()
    {
        _followBullet.IsDelayed += (isDelay) =>
        {
            if (isDelay == true)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        };
    }
}