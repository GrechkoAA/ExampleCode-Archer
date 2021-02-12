using UnityEngine;

[RequireComponent(typeof(FollowBullet))]
public class Vibration : MonoBehaviour
{
    [SerializeField] private FollowBullet _followBullet;

    private void OnEnable()
    {
        _followBullet.Stoped += () => Handheld.Vibrate();
    }
}