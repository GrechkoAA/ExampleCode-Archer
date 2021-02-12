using UnityEngine;

[RequireComponent(typeof(FollowBullet))]
public class SlowDownSound : MonoBehaviour
{
    [SerializeField] private FollowBullet _followBullet;
    [SerializeField] private UnityEngine.Audio.AudioMixerSnapshot _normal;
    [SerializeField] private UnityEngine.Audio.AudioMixerSnapshot _minPitch;
    [SerializeField] private float _minPinch;
    [SerializeField] private float _maxPinch;

    private void OnEnable()
    {
        _followBullet.IsDelayed += (isSlow) =>
         {
             if (isSlow == true)
             {
                 _minPitch.TransitionTo(_minPinch);
             }
             else
             {
                 _normal.TransitionTo(_maxPinch);
             }
         };
    }
}